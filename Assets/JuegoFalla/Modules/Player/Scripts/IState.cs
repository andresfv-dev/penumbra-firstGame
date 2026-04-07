using UnityEngine;

public interface IState
{
    void Enter();
    void Exit();
    void FixedUpdate();
    void Update();
}
