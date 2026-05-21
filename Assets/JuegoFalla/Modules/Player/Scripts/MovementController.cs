using UnityEngine;

public class MovementController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Settings")]

    public float gravity = -9.81f;
    [Header("Ground Check")]
    public float groundCheckRadius = 0.4f;
    public float groundCheckDistance = 0.1f;
    public Transform groundTransform;
    public LayerMask groundLayer;
    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    //Este método lo llamará el PlayerState o una IA de enemigo
    public void Move(Vector2 input, float speed)
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        characterController.Move(speed * Time.deltaTime * move);

        // Aplicar gravedad
        ApplyGravity();
    }

    public void Move(Vector2 input, float speed, Transform cameraTransform)
    {
        if (input.sqrMagnitude < 0.01f) return;

        // 1. Obtener los vectores de la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 2. "Aplanar" los vectores (importante para que no camine hacia el suelo)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // 3. Calcular dirección deseada
        Vector3 desiredDirection = forward * input.y + right * input.x;

        // 4. Rotar suavemente hacia la dirección de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);

        // 5. Mover
        characterController.Move(desiredDirection * speed * Time.deltaTime);
    }

    // Movimiento para First-Person: NO rota el transform del jugador,
    // se mueve relativo a la orientación de la cámara.
    public void MoveFP(Vector2 input, float speed, Transform cameraTransform)
    {
        if (input.sqrMagnitude < 0.01f) return;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 desiredDirection = forward * input.y + right * input.x;

        // En FP no rotamos el transform del jugador; la cámara controla la orientación
        characterController.Move(desiredDirection * speed * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (velocity.y < 0 && IsGrounded())
        {
            velocity.y = -2f; // Pequeña fuerza para mantener al personaje pegado al suelo
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundTransform.position, groundCheckRadius, groundLayer);
    }

    public bool IsMoving()
    {
        return characterController.velocity.magnitude > 0.1f;
    }



#if UNITY_EDITOR
    //Método de gizmos para visualizar el área de chequeo de suelo en el editor
    private void OnDrawGizmosSelected()
    {
        if (groundTransform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundTransform.position, groundCheckRadius);
    }
#endif
}
