using UnityEngine;
using UnityEngine.UI;

namespace Assets.JuegoFalla.UI
{
    // UI simple para mostrar el prompt de interacción (ej: "Presiona E para abrir")
    public class InteractionPromptUI : MonoBehaviour
    {
        public Text promptText;
        public CanvasGroup canvasGroup;

        private void Awake()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup != null) canvasGroup.alpha = 0f;
        }

        public void Show(string text)
        {
            if (promptText != null) promptText.text = text;
            if (canvasGroup != null) canvasGroup.alpha = 1f;
        }

        public void Hide()
        {
            if (canvasGroup != null) canvasGroup.alpha = 0f;
        }
    }
}
