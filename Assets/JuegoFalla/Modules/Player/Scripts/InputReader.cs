using UnityEngine;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(menuName = "Project/Input Reader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
{
    // Eventos para acciones discretas (Saltar, Disparar)
    public event Action JumpEvent = delegate { };
    public event Action FireEvent = delegate { };

    // Valores para acciones continuas (Moverse, Mirar)
    public Vector2 MoveValue { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsSprinting { get; private set; }
    [SerializeField] private bool useToggleSprint; // Esto lo puedes cambiar desde opciones


    private PlayerInputActions _inputActions;
    public PlayerInputActions InputActions => _inputActions;

    public void Initialize()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }
        _inputActions.Enable();
    }

    // Implementación de la interfaz generada por Unity
    public void OnMove(InputAction.CallbackContext context) => MoveValue = context.ReadValue<Vector2>();
    public void OnJump(InputAction.CallbackContext context) { if (context.performed) JumpEvent.Invoke(); }
    public void OnFire(InputAction.CallbackContext context) { if (context.performed) FireEvent.Invoke(); }
    public void OnAttack(InputAction.CallbackContext context) { IsAttacking = context.ReadValueAsButton(); }



    public void OnSprint(InputAction.CallbackContext context)
    {
        if (useToggleSprint)
        {
            if (context.performed) IsSprinting = !IsSprinting;
        }
        else
        {
            if (context.performed) IsSprinting = true;
            if (context.canceled) IsSprinting = false;
        }
    }
}