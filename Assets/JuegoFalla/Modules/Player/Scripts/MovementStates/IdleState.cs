using UnityEngine;

public class IdleState : MovementBaseState
{
    public IdleState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        // Aquí puedes poner la animación de idle, por ejemplo
        player.Anim.SetFloat("Speed", 0f);
    }

    public override void Update()
    {
        Vector2 input = player.Inputs.Player.Move.ReadValue<Vector2>();
        // Si el jugador empieza a moverse, cambiamos al estado de movimiento
        if (input.sqrMagnitude > 0.01f)
        {
            player.MovementSM.ChangeState(new MoveState(player));
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
