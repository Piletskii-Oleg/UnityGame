using System;
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

        private Transform camTransform;

        private void Start()
        {
            camTransform = cam.transform;
        }

        /// <summary>
        /// Called when the player wants to interact with something.
        /// </summary>
        public void Interact()
        {
            var ray = new Ray(camTransform.position, camTransform.forward);
            if (Physics.Raycast(ray, out var hitInfo, rayDistance, layerMask))
            {
                if (hitInfo.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact();
                }
            }
        }

        private void Update()
        {
            var ray = new Ray(camTransform.position, camTransform.forward);
            if (Physics.Raycast(ray, out var hitInfo, rayDistance, layerMask))
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