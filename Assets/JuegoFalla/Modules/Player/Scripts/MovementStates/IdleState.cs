using UnityEngine;

public class IdleState : MovementBaseState
{
    public IdleState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // Aquí puedes poner la animación de idle, por ejemplo
        player.anim.SetFloat("Speed", 0f);
    }

    public override void Update()
    {
        Vector2 input = player.inputs.MoveValue;
        // Si el jugador empieza a moverse, cambiamos al estado de movimiento
        if (player.inputs.IsSprinting && player.stats.CanSprint)
        {
            player.MovementSM.ChangeState(new RunState(player));
        }
        if (input.sqrMagnitude > 0.01f)
        {
            player.MovementSM.ChangeState(new WalkState(player));
        }
    }

    public override void Exit()
    {
        // No hay nada específico que hacer al salir del estado de idle
    }

    public override void FixedUpdate()
    {
        // No hay física específica que manejar en el estado de idle
    }
}
