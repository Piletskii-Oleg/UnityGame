using DataPersistence;
using DataPersistence.DataFiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    /// <summary>
    /// Class that processes mouse input (used with <see cref="InputController"/>)
    /// </summary>
    public class PlayerLook : MonoBehaviour, IDataPersistence<OptionsData>, IDataPersistence<GameData>
    {
        [FormerlySerializedAs("options")] [SerializeField]
        private OptionsDataManager optionsData;

        private float sensitivity;

        private Camera cam;

        private float verticalRotation;

        private void Awake()
        {
            sensitivity = optionsData.MouseSensitivity;
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

            verticalRotation -= mouseY * sensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            transform.Rotate(mouseX * sensitivity * Vector3.up);
        }

        public void OnSave(GameData data)
            => data.cameraRotation = cam.transform.rotation;

        public void OnLoad(GameData data)
            => cam.transform.rotation = data.cameraRotation;

        public void OnSave(OptionsData data)
            => data.mouseSensitivity = sensitivity;

        public void OnLoad(OptionsData data)
            => sensitivity = data.mouseSensitivity;
    }
}
