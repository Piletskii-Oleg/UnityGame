using UnityEngine;

/// <summary>
/// Makes the player interact with <see cref="Interactable"/> gameobjects.
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float rayDistance = 3.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera cam;

    /// <summary>
    /// Called when the player wants to interact with something.
    /// </summary>
    public void Interact()
    {
        var ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, layerMask))
        {
            if (hitInfo.collider.TryGetComponent<Interactable>(out var interactable))
            {
                interactable.BaseInteract();
            }
        }
    }
}
