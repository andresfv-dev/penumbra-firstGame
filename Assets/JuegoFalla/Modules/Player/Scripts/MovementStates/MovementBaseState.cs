using UnityEngine;

public abstract class MovementBaseState : IState
{
    protected PlayerController player;
    protected InputReader input;
    protected MovementController movementController;
    public MovementBaseState(PlayerController player)
    {
        this.player = player;
        this.input = player.Inputs;
        movementController = player.GetComponent<MovementController>();
    }
    public abstract void Enter();
    public abstract void Update();

    public abstract void Exit();

    public abstract void FixedUpdate();


}
