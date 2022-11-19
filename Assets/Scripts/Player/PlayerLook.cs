using UnityEngine;

namespace Player
{
    /// <summary>
    /// Class that processes mouse input (used with <see cref="InputManager"/>)
    /// </summary>
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity;

        private Camera cam;

        private float verticalRotation;

        private void Start()
        {
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        /// <summary>
        /// Move the camera according to mouse input.
        /// </summary>
        /// <param name="input">Mouse input from <see cref="InputManager"/></param>
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
}
