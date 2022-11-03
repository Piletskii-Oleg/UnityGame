using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;

    private Camera cam;

    private float verticalRotation = 0f;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        verticalRotation -= (mouseY * Time.deltaTime) * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        transform.Rotate((mouseX * Time.deltaTime) * mouseSensitivity * Vector3.up);
    }
}
