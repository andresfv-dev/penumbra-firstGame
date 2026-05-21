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

        player.anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);

        // Si el jugador quiere sprintar y tiene stamina, transicionar a RunState
        if (player.inputs.IsSprinting && player.stats.CanSprint)
        {
            player.MovementSM.ChangeState(new RunState(player));
            return;
        }

        // Movemos al personaje velocidad de caminata
        movementController.MoveFP(input, player.statsConfig.walkSpeed, player.MainCameraTransform);

        // Regenerar stamina si no está sprintando
        if (!player.inputs.IsSprinting)
        {
            player.stats.RegenerateStamina(player.statsConfig.staminaRegenRate, player.statsConfig.maxStamina);
        }

        if (input == Vector2.zero)
        {
            player.MovementSM.ChangeState(new IdleState(player));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Walk State");
    }

    public override void FixedUpdate()
    {
        // No se necesita lógica adicional aquí; MoveFP se llama desde Update
    }
}
