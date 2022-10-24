using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement movement;
    private PlayerLook look;
    private WeaponManager weaponManager;
    private Health health;

    private bool isShooting;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();
        health = GetComponent<Health>();

        SubscribeToEvents();
    }

    private void FixedUpdate()
    {
        movement.ProcessHorizontalMovement(onFoot.Movement.ReadValue<Vector2>());
        movement.ProcessVerticalMovement();

        if (isShooting)
        {
            weaponManager.Shoot();
        }
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

        onFoot.Shoot.started += _ => isShooting = true;
        onFoot.Shoot.canceled += _ => isShooting = false;

        onFoot.Reload.performed += _ => weaponManager.StartReload();

        onFoot.ChangeWeapon.performed += _ => weaponManager.ChangeWeapon((int)onFoot.ChangeWeapon.ReadValue<float>());

        // health debugging
        onFoot.Jump.performed += ctx => health.TakeDamage(30);
        onFoot.Reload.performed += ctx => health.Heal(30);
    }
}
