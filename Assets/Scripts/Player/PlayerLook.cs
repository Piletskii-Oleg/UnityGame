using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private enum RotationAxes
    {
        Horizontal,
        HorizontalAndVertical
    };

    [SerializeField] private RotationAxes direction;

    [SerializeField] private float mouseSensitivity = 0.5f;

    private float verticalRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var horizontalDelta = Input.GetAxis("Mouse X") * mouseSensitivity;
        if (direction == RotationAxes.Horizontal)
        {
            transform.Rotate(0, horizontalDelta, 0);
        }
        else
        {
            var horizontalRotation = transform.localEulerAngles.y + horizontalDelta;

            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            transform.localEulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);
        }
    }
}
