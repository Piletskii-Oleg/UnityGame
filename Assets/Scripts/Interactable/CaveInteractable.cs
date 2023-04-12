using UnityEngine;

namespace Interactable
{
    public class CaveInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptMessage;
        
        public void Interact()
        {
            
        }

        public string GetPromptMessage() => promptMessage;
    }
}