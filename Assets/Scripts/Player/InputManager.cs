using UnityEngine;

/// <summary>
/// An input manager for a single-player game built using new Input System.
/// </summary>
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement movement;
    private PlayerLook look;
    private WeaponManager weaponManager;
    private WeaponZoom weaponZoom;

    private bool isShooting;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();
        weaponZoom = GetComponent<WeaponZoom>();

        SubscribeToEvents();
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            weaponManager.Shoot();
        }

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

    private void SubscribeToEvents()
    {
        onFoot.Jump.performed += _ => movement.Jump();

        onFoot.Run.started += _ => movement.StartRunning();
        onFoot.Run.canceled += _ => movement.StopRunning();

        onFoot.Shoot.started += _ => isShooting = true;
        onFoot.Shoot.canceled += _ => isShooting = false;

        onFoot.Reload.performed += _ => weaponManager.StartReload();

        onFoot.ChangeWeapon.performed += _ => weaponManager.ChangeWeapon((int)onFoot.ChangeWeapon.ReadValue<float>());

        onFoot.Zoom.started += _ => weaponZoom.Zoom(true);
        onFoot.Zoom.canceled += _ => weaponZoom.Zoom(false);
        // health debugging
        // onFoot.Jump.performed += ctx => health.TakeDamage(30);
        // onFoot.Reload.performed += ctx => health.Heal(30);
    }
}
