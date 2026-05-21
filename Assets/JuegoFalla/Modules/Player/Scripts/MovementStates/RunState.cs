using UnityEngine;

public class RunState : MovementBaseState
{
    public RunState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        //Aquí puedes poner cualquier lógica que quieras que suceda al entrar en el estado de correr, como cambiar animaciones, etc.

    }

    public override void Update()
    {
        Vector2 input = player.inputs.MoveValue;

        // Si no hay input, volvemos a Idle
        if (input == Vector2.zero)
        {
            player.MovementSM.ChangeState(new IdleState(player));
            return;
        }

        // Si no está sprintando o se quedó sin stamina, volvemos a WalkState
        if (!player.inputs.IsSprinting || !player.stats.CanSprint)
        {
            player.MovementSM.ChangeState(new WalkState(player));
            return;
        }

        // Ejecutar movimiento a velocidad de sprint y consumir stamina
        movementController.MoveFP(input, player.statsConfig.sprintSpeed, player.MainCameraTransform);
        player.anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        player.stats.ConsumeStamina(player.statsConfig.staminaBurnRate);
    }

    public override void Exit()
    {
        //Aquí puedes poner cualquier lógica que quieras que suceda al salir del estado de correr, como cambiar animaciones, etc.
        //player.anim.SetBool("IsRunning", false);
    }

    public override void FixedUpdate()
    {

    }
}

