using UnityEngine;

public abstract class ActionBaseState : IState
{
    protected PlayerController player;
    protected MovementController movementController;
    protected InputReader input;
    public ActionBaseState(PlayerController player)
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
