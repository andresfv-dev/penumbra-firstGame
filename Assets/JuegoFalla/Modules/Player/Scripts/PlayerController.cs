using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public StateMachine MovementSM { get; private set; }
    public StateMachine ActionSM { get; private set; }

    // Referencias a componentes
    public CharacterController Controller;
    public Animator Anim;
    public InputReader Inputs;

    private void Awake()
    {
        MovementSM = new StateMachine();
        ActionSM = new StateMachine();

    }

    private void OnEnable()
    {
        Inputs.Initialize();
        Inputs.FireEvent += () => ActionSM.ChangeState(new FireState(this));
    }

    private void OnDisable()
    {
        Inputs.FireEvent -= () => ActionSM.ChangeState(new FireState(this));
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

        //Debug.Log($"MoveState received input: {Inputs.MoveValue}");
        //Debug.Log($"Input magnitude: {Inputs.MoveValue.magnitude}");
    }
}
