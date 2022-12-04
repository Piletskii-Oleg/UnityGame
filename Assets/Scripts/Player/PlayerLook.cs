using System;
using DataPersistence;
using DataPersistence.DataFiles;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Class that processes mouse input (used with <see cref="InputController"/>)
    /// </summary>
    public class PlayerLook : MonoBehaviour, IGameDataPersistence, IOptionsDataPersistence
    {
        [SerializeField] private float mouseSensitivity;

        private Camera cam;

        private float verticalRotation;

        private void Awake()
        {
            cam = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        /// <summary>
        /// Move the camera according to mouse input.
        /// </summary>
        /// <param name="input">Mouse input from <see cref="InputController"/></param>
        public void ProcessLook(Vector2 input)
        {
            var mouseX = input.x;
            var mouseY = input.y;

            verticalRotation -= mouseY * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            transform.Rotate(mouseX * mouseSensitivity * Vector3.up);
        }

        public void OnSave(GameData data)
            => data.cameraRotation = cam.transform.rotation;

        public void OnLoad(GameData data)
            => cam.transform.rotation = data.cameraRotation;

        public void SaveOptionsToFile(OptionsData data)
        {
            data.mouseSensitivity = mouseSensitivity;
        }

        public void LoadOptionsFromFile(OptionsData data)
        {
            mouseSensitivity = data.mouseSensitivity;
        }
    }
}
