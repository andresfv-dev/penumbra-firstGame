using UnityEngine;

public class NeutralState : ActionBaseState
{
    public NeutralState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // Aquí podrías poner una animación de "neutral" o algo similar
        player.Anim.SetBool("IsAttacking", false);
    }

    public override void Update()
    {
        // En este estado no hacemos nada, solo esperamos a que el jugador ataque
        if (player.Inputs.InputActions.Player.Attack.WasPressedThisFrame())
        {
            player.ActionSM.ChangeState(new AttackState(player));
        }
    }

    public override void Exit()
    {
        // No hay nada específico que hacer al salir de este estado
    }

    public override void FixedUpdate()
    {
        // No hay física específica que manejar en este estado
    }
}
