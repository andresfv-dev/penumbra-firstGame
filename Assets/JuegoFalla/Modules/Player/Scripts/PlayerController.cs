using Unity.Cinemachine;
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
    [Header("Camera References")]
    [SerializeField] private CinemachineCamera virtualCamera; // Para control específico

    // Cache para el estado de fuego (reutilizar la instancia)
    private FireState _fireState;

    [Header("Rotation")]
    [Tooltip("Smooth factor for syncing player yaw to camera (higher = faster)")]
    public float rotationSmooth = 10f;
    [Tooltip("Only rotate the player toward camera when there's movement input")]
    public bool rotateOnlyWhenMoving = true;

    // Lo que realmente necesitamos para el movimiento relativo:
    public Transform MainCameraTransform { get; private set; }

    private void Awake()
    {
        MovementSM = new StateMachine();
        ActionSM = new StateMachine();

        // Caché de la cámara principal (más eficiente que Camera.main en cada frame)
        if (Camera.main != null)
            MainCameraTransform = Camera.main.transform;

    }

    private void OnEnable()
    {
        inputs.Initialize();
        inputs.FireEvent += OnFire;
    }

    private void OnDisable()
    {
        inputs.FireEvent -= OnFire;
    }

    private void OnFire()
    {
        // Reutilizamos la instancia del estado para evitar crear objetos constantemente
        if (_fireState == null) _fireState = new FireState(this);
        ActionSM.ChangeState(_fireState);
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

        // Sincronizar yaw del jugador con la cámara controlada por Cinemachine
        // Rotamos suavemente solo en Y (yaw). Opcional: solo cuando hay input de movimiento.
        if (MainCameraTransform != null)
        {
            bool shouldRotate = true;
            if (rotateOnlyWhenMoving)
            {
                shouldRotate = inputs.MoveValue.sqrMagnitude > 0.01f;
            }

            if (shouldRotate)
            {
                float targetYaw = MainCameraTransform.eulerAngles.y;
                Quaternion targetRot = Quaternion.Euler(0f, targetYaw, 0f);
                // Suavizado con Slerp
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSmooth);
            }
        }

        movementController.ApplyGravity();
        //Debug.Log($"MoveState received input: {Inputs.MoveValue}");
        //Debug.Log($"Input magnitude: {Inputs.MoveValue.magnitude}");
    }
}
