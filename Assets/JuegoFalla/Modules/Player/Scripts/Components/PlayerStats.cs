using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    [Header("Stats Actuales")]
    public float currentHealth = 100f;
    public float currentStamina = 100f;
    public float currentHunger = 100f;
    public float currentThirst = 100f;
    public float currentSpeed;
    public bool CanSprint => currentStamina > 0;
    public void ConsumeStamina(float amount)
    {
        currentStamina = Mathf.Max(0f, currentStamina - amount * Time.deltaTime);
    }

    public void RegenerateStamina(float amount, float maxStamina)
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + amount * Time.deltaTime);
    }

    // Evento simple para notificar cambios de stamina (puede ser útil para UI)
    public event System.Action<float> OnStaminaChanged = delegate { };

    // Emitir evento solo cuando cambia la stamina para evitar sobrecarga
    private float _lastStaminaSent = -1f;
    private void LateUpdate()
    {
        if (!Mathf.Approximately(_lastStaminaSent, currentStamina))
        {
            _lastStaminaSent = currentStamina;
            OnStaminaChanged.Invoke(currentStamina);
        }
    }

    public void Awake()
    { }
}
