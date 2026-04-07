using UnityEngine;

public class FireState : ActionBaseState
{
    public FireState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // Aquí podrías poner una animación de "fuego" o algo similar
        player.Anim.SetBool("IsFiring", true);
        Debug.Log("Entrando al estado de disparo");
    }

    public override void Update()
    {
        // En este estado estamos "disparando", podrías manejar la lógica de disparo aquí, como instanciar proyectiles, etc.
        // Por simplicidad, vamos a volver al estado neutral después de un tiempo o cuando se suelte el botón de disparo
        if (player.Inputs.Player.Fire.WasReleasedThisFrame())
        {
            player.ActionSM.ChangeState(new NeutralState(player));
        }
    }

    public override void Exit()
    {
        // No hay nada específico que hacer al salir de este estado
        player.Anim.SetBool("IsFiring", false);
    }

    public override void FixedUpdate()
    {

    }


}
