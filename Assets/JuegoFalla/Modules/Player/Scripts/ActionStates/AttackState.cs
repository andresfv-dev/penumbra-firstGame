using UnityEngine;

public class AttackState : ActionBaseState
{
    public AttackState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // Aquí podrías poner una animación de ataque o algo similar
        player.Anim.SetTrigger("IsAttacking");
        Debug.Log("Entrando al estado de ataque");
    }

    public override void Update()
    {
        // Aquí podrías manejar la lógica de ataque, como detectar colisiones con enemigos, etc.
        // Por simplicidad, vamos a volver al estado neutral después de un tiempo o cuando se suelte el botón de ataque
        if (player.Inputs.Player.Attack.WasReleasedThisFrame())
        {
            player.ActionSM.ChangeState(new NeutralState(player));
        }
    }

    public override void Exit()
    {
        // Aquí podrías limpiar cualquier estado relacionado con el ataque si es necesario
        //player.Anim.SetBool("IsAttacking", false);
    }

    public override void FixedUpdate()
    {
        // Si tu ataque tiene alguna física específica, como un impulso hacia adelante, podrías manejarlo aquí
    }
}
