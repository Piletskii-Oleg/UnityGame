using UnityEngine;

namespace Interactable
{
    public class ButtonInteractable : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacting with " + gameObject.name);
        }
    }
}