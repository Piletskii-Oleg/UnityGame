using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement movement;
    private PlayerLook look;
    private WeaponManager weaponManager;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();

        onFoot.Jump.performed += ctx => movement.Jump();

        onFoot.ShootPrimary.performed += ctx => weaponManager.Shoot();
        onFoot.Reload.performed += ctx => weaponManager.Reload();
    }

    private void FixedUpdate()
    {
        movement.ProcessHorizontalMovement(onFoot.Movement.ReadValue<Vector2>());
        movement.ProcessVerticalMovement();
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
