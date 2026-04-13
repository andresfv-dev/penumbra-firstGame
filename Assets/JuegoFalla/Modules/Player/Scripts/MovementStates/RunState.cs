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

        // 1. Condición de salida: Dejó de moverse
        if (input == Vector2.zero)
        {
            player.MovementSM.ChangeState(new IdleState(player));
            return;
        }

        // 2. Condición de salida: Soltó el botón o se cansó
        // Nota: player.Stats es un COMPONENTE, no el SO directamente
        if (!player.inputs.IsSprinting || player.stats.currentStamina <= 0)
        {
            player.MovementSM.ChangeState(new WalkState(player));
            return;
        }

        // 3. Ejecutar Movimiento
        movementController.Move(input, player.statsConfig.sprintSpeed, player.MainCameraTransform);

        // 4. Actualizar Animator (Normalizado: 1 = Correr)
        player.anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);

        // 5. Consumir Estamina
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

