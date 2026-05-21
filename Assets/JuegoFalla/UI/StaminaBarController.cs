using UnityEngine;
using UnityEngine.UI;

// Controla una barra de stamina que se consume desde ambos bordes hacia el centro.
// Requiere dos RectTransforms hijos: leftFill (anclado a la izquierda, pivot x=0)
// y rightFill (anclado a la derecha, pivot x=1). Ambos deben tener la misma altura.
public class StaminaBarController : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform container; // contenedor de la barra (usa su width)
    public RectTransform leftFill;   // anclado a la izquierda (pivot 0.5 o 0)
    public RectTransform rightFill;  // anclado a la derecha (pivot 1)

    [Header("Player Stats")]
    public PlayerStats playerStats; // referencia al componente PlayerStats
    public PlayerStatsSO statsConfig; // para obtener maxStamina

    [Header("Smoothing")]
    public float smoothSpeed = 10f;

    float shownNormalized = 1f;

    void Start()
    {
        if (container == null) container = GetComponent<RectTransform>();
        if (playerStats == null)
        {
            Debug.LogWarning("StaminaBarController: playerStats no asignado.");
        }
        if (statsConfig == null && playerStats != null)
        {
            Debug.LogWarning("StaminaBarController: statsConfig no asignado. Usa playerStatsConfig manualmente.");
        }

        // Suscribirse al evento para actualización instantánea
        if (playerStats != null)
        {
            playerStats.OnStaminaChanged += OnStaminaChanged;
        }
    }

    void Update()
    {
        if (playerStats == null || statsConfig == null || container == null || leftFill == null || rightFill == null) return;

        float target = Mathf.Clamp01(playerStats.currentStamina / statsConfig.maxStamina);
        shownNormalized = Mathf.MoveTowards(shownNormalized, target, Time.deltaTime * smoothSpeed);

        float totalWidth = container.rect.width;
        float halfWidth = totalWidth * shownNormalized * 0.5f;

        Vector2 leftSize = leftFill.sizeDelta;
        Vector2 rightSize = rightFill.sizeDelta;

        leftSize.x = halfWidth;
        rightSize.x = halfWidth;

        leftFill.sizeDelta = leftSize;
        rightFill.sizeDelta = rightSize;
    }

    private void OnDestroy()
    {
        if (playerStats != null)
            playerStats.OnStaminaChanged -= OnStaminaChanged;
    }

    private void OnStaminaChanged(float value)
    {
        // Actualiza instantáneamente el objetivo para que el UI responda incluso fuera de Update
        float target = Mathf.Clamp01(value / statsConfig.maxStamina);
        shownNormalized = target; // permitimos que Update haga smoothing desde este valor
    }

    // Ajuste rápido por código si necesitas actualizar instantáneamente
    public void SetNormalizedInstant(float normalized)
    {
        shownNormalized = Mathf.Clamp01(normalized);
        float totalWidth = container.rect.width;
        float halfWidth = totalWidth * shownNormalized * 0.5f;
        Vector2 leftSize = leftFill.sizeDelta;
        Vector2 rightSize = rightFill.sizeDelta;
        leftSize.x = halfWidth;
        rightSize.x = halfWidth;
        leftFill.sizeDelta = leftSize;
        rightFill.sizeDelta = rightSize;
    }
}
