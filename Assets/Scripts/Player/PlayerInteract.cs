using UnityEngine;
using Interactable;
using UnityEngine.Events;

namespace Player
{
    /// <summary>
    /// Makes the player interact with <see cref="Interactable"/> game objects.
    /// </summary>
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float rayDistance = 3.0f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Camera cam;

        [SerializeField] private UnityEvent<string> onSeeInteractable;
        [SerializeField] private UnityEvent onLookAwayFromInteractable;

        /// <summary>
        /// Called when the player wants to interact with something.
        /// </summary>
        public void Interact()
        {
            var ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, layerMask))
            {
                if (hitInfo.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact();
                }
            }
        }

        private void Update()
        {
            var ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, layerMask))
            {
                if (hitInfo.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    onSeeInteractable.Invoke(interactable.GetPromptMessage());
                }
            }
            else
            {
                onLookAwayFromInteractable.Invoke();
            }
        }
    }
}