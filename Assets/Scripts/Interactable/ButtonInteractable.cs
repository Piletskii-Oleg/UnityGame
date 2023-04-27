using UnityEngine;
using UnityEngine.Events;

namespace Interactable
{
    public class ButtonInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptMessage;

        [SerializeField] private UnityEvent onPress;
        
        public void Interact()
            => onPress.Invoke();

        public string GetPromptMessage()
            => promptMessage;
    }
}