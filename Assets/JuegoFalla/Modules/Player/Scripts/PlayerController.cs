using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public StateMachine MovementSM { get; private set; }
    public StateMachine ActionSM { get; private set; }

    // Referencias a componentes
    public CharacterController controller;
    public Animator anim;
    public InputReader inputs;
    public PlayerStatsSO statsConfig; //Scriptable Object con los valores base
    public PlayerStats stats; //Componente con los valores actuales
    public MovementController movementController;

    private void Awake()
    {
        MovementSM = new StateMachine();
        ActionSM = new StateMachine();

    }

    private void OnEnable()
    {
        inputs.Initialize();
        inputs.FireEvent += () => ActionSM.ChangeState(new FireState(this));
    }

    private void OnDisable()
    {
        inputs.FireEvent -= () => ActionSM.ChangeState(new FireState(this));
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

        movementController.ApplyGravity();
        //Debug.Log($"MoveState received input: {Inputs.MoveValue}");
        //Debug.Log($"Input magnitude: {Inputs.MoveValue.magnitude}");
    }
}
