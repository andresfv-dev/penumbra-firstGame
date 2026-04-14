public interface IInteractable
{
    string interactionPrompt { get; } //El mensaje que aparecerá en la UI
    void Interact(PlayerController player); //Este método ejecutará la acción
}