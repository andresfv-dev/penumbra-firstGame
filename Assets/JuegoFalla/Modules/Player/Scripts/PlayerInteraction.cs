using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    [SerializeField] private float interactionRange = 2.0f;
    [SerializeField] private float detectionRadius = 0.2f;
    [SerializeField] private LayerMask interactLayer;

    [Header("Referencias")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform mainCameraTransform; // Necesitamos el transform de la cámara
    [SerializeField] private InteractionPromptUI promptUI;

    private IInteractable _currentInteractable;

    private void OnEnable() => playerController.inputs.InteractEvent += HandleInteract;
    private void OnDisable() => playerController.inputs.InteractEvent -= HandleInteract;

    private void Awake()
    {
        // Auto-assign camera transform if not set in inspector
        if (mainCameraTransform == null && playerController != null)
        {
            mainCameraTransform = playerController.MainCameraTransform;
        }

        if (mainCameraTransform == null && Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }

        // If interactionPoint is not assigned, try to create or find one as child of camera
        if (interactionPoint == null && mainCameraTransform != null)
        {
            // Try to find a child named "InteractionPoint" under the camera
            var t = mainCameraTransform.Find("InteractionPoint");
            if (t != null) interactionPoint = t;
        }
    }

    private void Update()
    {
        RotatePointWithCamera();
        CheckForInteractables();
    }

    private void RotatePointWithCamera()
    {
        if (mainCameraTransform == null || interactionPoint == null) return;

        // Hacemos que el punto de interacción mire exactamente hacia donde mira la cámara.
        // Esto sincroniza tanto el arriba/abajo como el izquierda/derecha.
        interactionPoint.rotation = mainCameraTransform.rotation;

        // OPCIONAL: Limitador de Ángulo (Survival Horror Style)
        // Si no quieres que el jugador interactúe con cosas que están "a su espalda" 
        // aunque la cámara las mire, podrías usar un Angle Clamp aquí.
    }


    private void CheckForInteractables()
    {
        RaycastHit hit;

        // SphereCast ahora detectará según hacia donde apunte la cámara
        bool hasHit = Physics.SphereCast(
            interactionPoint.position,
            detectionRadius,
            interactionPoint.forward,
            out hit,
            interactionRange,
            interactLayer
        );

        if (hasHit && hit.collider.TryGetComponent(out IInteractable interactable))
        {
            // Calculamos la dirección desde el jugador hacia el objeto
            Vector3 dirToTarget = (hit.point - transform.position).normalized;

            // Comprobamos si el objeto está en un ángulo frontal (ej. 90 grados)
            // 0.5f significa un cono de unos 60-90 grados aproximadamente
            float dot = Vector3.Dot(transform.forward, dirToTarget);
            if ((_currentInteractable != interactable || _currentInteractable == null) && dot > 0.5f)
            {
                _currentInteractable = interactable;
                Debug.Log($"<color=cyan>Objetivo:</color> {_currentInteractable.interactionPrompt}");
                if (promptUI != null)
                {
                    promptUI.Show(_currentInteractable.interactionPrompt);
                }
            }
        }
        else
        {
            _currentInteractable = null;
            if (promptUI != null) promptUI.Hide();
        }
    }

    private void HandleInteract() => _currentInteractable?.Interact(playerController);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (interactionPoint == null) return;

        Gizmos.color = (_currentInteractable != null) ? Color.green : Color.yellow;
        Vector3 endPoint = interactionPoint.position + (interactionPoint.forward * interactionRange);

        // El Gizmo ahora se moverá cuando muevas la cámara en modo Play
        Gizmos.DrawLine(interactionPoint.position, endPoint);
        Gizmos.DrawWireSphere(endPoint, detectionRadius);
    }
#endif
}
