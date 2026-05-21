using UnityEngine;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(menuName = "Project/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    //Aqui se agregan todas las variables relacionadas con las estadisticas del jugador
    //También se pueden agregar restricciones en caso que quieras que el jugador no pueda superar cierto valor, o que se regenere con el tiempo, etc.
    //1. Componentes determinados
    [Header("Maximos")]
    public float maxHealth = 100f;
    public float maxStamina = 100f;
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    // Eliminado jumpForce porque no se soporta salto en la experiencia FP actual
    // public float jumpForce = 5f;
    public float maxHunger = 100f;
    public float maxThirst = 100f;



    //2. Restricciones 
    [Header("Restricciones")]
    public float staminaBurnRate = 20f; // Cuánto se quema la stamina por segundo al sprintar
    public float staminaRegenRate = 10f; // Cuánto se regenera la stamina por segundo cuando no se está sprintando
    public float hungerBurnRate = 5f; // Cuánto se quema el hambre por segundo
    public float thirstBurnRate = 5f; // Cuánto se quema la sed por segundo

}
