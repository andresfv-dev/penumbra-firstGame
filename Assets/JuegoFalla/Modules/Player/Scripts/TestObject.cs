using UnityEngine;

public class TestObject : MonoBehaviour, IInteractable
{
    public string interactionPrompt => "Abrir Puerta de Prueba";
    public void Interact(PlayerController player) => Debug.Log($"<color=green>Interacción:</color> ¡Puerta abierta con éxito!");
}