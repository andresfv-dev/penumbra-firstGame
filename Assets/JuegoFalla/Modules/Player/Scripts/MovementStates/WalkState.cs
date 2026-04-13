using UnityEngine;

public class WalkState : MovementBaseState
{
    public WalkState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        //Debug.Log("Entering Walk State"); 
    }

    public override void Update()
    {
        //Leemos el input desde el PlayerController
        Vector2 input = player.inputs.MoveValue;
        float inputMagnitude = input.magnitude;

        player.anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);

        //Movemos al personaje usando el MovementController
        movementController.Move(input, player.statsConfig.walkSpeed, player.MainCameraTransform);
        if (input == Vector2.zero)
        {
            player.MovementSM.ChangeState(new IdleState(player));
        }
        if (player.inputs.IsSprinting && player.stats.CanSprint)
        {
            player.MovementSM.ChangeState(new RunState(player));
        }


    }

    public override void Exit()
    {
        Debug.Log("Exiting Walk State");
    }

    public override void FixedUpdate()
    {
        // Aquí iría la lógica de movimiento, por ejemplo:
        // movementController.Move(player.Inputs.MovementInput);
    }


}
