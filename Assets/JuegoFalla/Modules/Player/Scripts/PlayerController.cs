using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public StateMachine MovementSM { get; private set; }
    public StateMachine ActionSM { get; private set; }

    // Referencias a componentes
    public CharacterController Controller;
    public Animator Anim;
    public PlayerInputActions Inputs; // Generado por el Input System

    private void Awake()
    {
        MovementSM = new StateMachine();
        ActionSM = new StateMachine();

    }

    private void OnEnable()
    {
        if (Inputs == null) Inputs = new PlayerInputActions();
        Inputs.Enable();
        Inputs.Player.Fire.performed += ctx => ActionSM.ChangeState(new FireState(this));
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }

    private void Start()
    {
        // Inicializamos las dos capas
        MovementSM.Initialize(new IdleState(this));
        ActionSM.Initialize(new NeutralState(this));
    }

    private void Update()
    {
        MovementSM.CurrentState.Update();
        ActionSM.CurrentState.Update();
    }
}
