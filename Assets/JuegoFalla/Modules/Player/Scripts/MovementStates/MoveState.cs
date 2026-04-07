using UnityEngine;

public class MoveState : MovementBaseState
{
    public MoveState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entering Move State");
    }

    public override void Update()
    {
        //Leemos el input desde el PlayerController
        Vector2 input = player.Inputs.Player.Move.ReadValue<Vector2>();
        Debug.Log($"MoveState received input: {input}");
        float inputMagnitude = input.magnitude;
        Debug.Log($"Input magnitude: {inputMagnitude}");
        player.Anim.SetFloat("Speed", inputMagnitude, 0.1f, Time.deltaTime);

        //Movemos al personaje usando el MovementController
        movementController.Move(input);
        if (input == Vector2.zero)
        {
            player.MovementSM.ChangeState(new IdleState(player));
        }




    }

    public override void Exit()
    {
        Debug.Log("Exiting Move State");
    }

    public override void FixedUpdate()
    {
        // Aquí iría la lógica de movimiento, por ejemplo:
        // movementController.Move(player.Inputs.MovementInput);
    }


}
