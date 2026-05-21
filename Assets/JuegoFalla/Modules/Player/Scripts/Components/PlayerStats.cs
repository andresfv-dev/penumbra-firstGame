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

    public void Awake()
    { }
}
