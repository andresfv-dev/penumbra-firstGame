using UnityEngine;
using UnityEngine.UI;

// Controla una barra de stamina usando UNA sola imagen que se reduce desde ambos bordes hacia el centro.
// Requiere:
// - container: RectTransform que define el ancho total de la barra
// - fill: RectTransform del image que estará centrada y reducirá su ancho
public class StaminaBarController : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform container; // contenedor de la barra (usa su width)
    public RectTransform fill;      // imagen única centrada que se hace más angosta

    [Header("Player Stats")]
    public PlayerStats playerStats; // referencia al componente PlayerStats
    public PlayerStatsSO statsConfig; // para obtener maxStamina

    [Header("Behavior")]
    [Range(0f, 0.2f)]
    public float minWidthFraction = 0.02f; // ancho mínimo relativo cuando stamina = 0
    public float smoothSpeed = 10f;

    float shownNormalized = 1f;

    void Start()
    {
        if (container == null) container = GetComponent<RectTransform>();
        if (playerStats == null) Debug.LogWarning("StaminaBarController: playerStats no asignado.");
        if (statsConfig == null && playerStats != null) Debug.LogWarning("StaminaBarController: statsConfig no asignado.");

        if (playerStats != null) playerStats.OnStaminaChanged += OnStaminaChanged;
    }

    void Update()
    {
        if (playerStats == null || statsConfig == null || container == null || fill == null) return;

        float target = Mathf.Clamp01(playerStats.currentStamina / statsConfig.maxStamina);
        shownNormalized = Mathf.MoveTowards(shownNormalized, target, Time.deltaTime * smoothSpeed);

        float totalWidth = container.rect.width;
        float clamped = Mathf.Max(minWidthFraction, shownNormalized);
        float newWidth = totalWidth * clamped;

        Vector2 size = fill.sizeDelta;
        size.x = newWidth;
        fill.sizeDelta = size;

        // Centrar fill: asumiendo anchors centrados, anchoredPosition = 0
        fill.anchoredPosition = Vector2.zero;
    }

    private void OnDestroy()
    {
        if (playerStats != null) playerStats.OnStaminaChanged -= OnStaminaChanged;
    }

    private void OnStaminaChanged(float value)
    {
        float target = Mathf.Clamp01(value / statsConfig.maxStamina);
        shownNormalized = target; // el smoothing en Update hace la animación
    }

    public void SetNormalizedInstant(float normalized)
    {
        shownNormalized = Mathf.Clamp01(normalized);
        if (container == null || fill == null) return;
        float totalWidth = container.rect.width;
        float clamped = Mathf.Max(minWidthFraction, shownNormalized);
        float newWidth = totalWidth * clamped;
        Vector2 size = fill.sizeDelta;
        size.x = newWidth;
        fill.sizeDelta = size;
        fill.anchoredPosition = Vector2.zero;
    }
}
