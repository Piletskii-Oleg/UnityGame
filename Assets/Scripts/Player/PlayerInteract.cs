using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float rayDistance = 3.0f;
    [SerializeField] private LayerMask layerMask;
    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        var ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, layerMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
            }
        }
    }
}
