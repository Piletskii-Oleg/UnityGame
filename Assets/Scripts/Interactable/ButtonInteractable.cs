using UnityEngine;

namespace Interactable
{
    public class ButtonInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptMessage;
        
        public void Interact()
        {
            Debug.Log("Interacting with " + gameObject.name);
        }

        public string GetPromptMessage()
            => promptMessage;
    }
}