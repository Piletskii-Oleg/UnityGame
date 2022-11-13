using UnityEngine;

/// <summary>
/// An input manager for a single-player game built using new Input System.
/// </summary>
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerInput.UIActions uiActions;

    private PlayerMovement movement;
    private PlayerLook look;
    private WeaponController weaponController;
    private PlayerInteract interact;
    private WeaponZoom weaponZoom;

    [SerializeField] private InventoryUI inventoryUI;

    private bool isShooting;
    
    private bool isUsingUI = false;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        uiActions = playerInput.UI;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        weaponController = GetComponent<WeaponController>();
        interact = GetComponent<PlayerInteract>();
        weaponZoom = GetComponentInChildren<WeaponZoom>();

        SubscribeToEvents();
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            weaponController.Shoot();
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
        uiActions.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
        uiActions.Disable();
    }

    private void SubscribeToEvents()
    {
        onFoot.Jump.performed += _ => movement.Jump();

        onFoot.Run.started += _ => movement.StartRunning();
        onFoot.Run.canceled += _ => movement.StopRunning();

        onFoot.Shoot.started += _ => isShooting = true;
        onFoot.Shoot.canceled += _ => isShooting = false;

        onFoot.Reload.performed += _ => weaponController.StartReload();

        onFoot.ChangeWeapon.performed += _ => weaponController.ChangeWeapon((int)onFoot.ChangeWeapon.ReadValue<float>());

        onFoot.Zoom.started += _ => weaponZoom.Zoom(true);
        onFoot.Zoom.canceled += _ => weaponZoom.Zoom(false);

        onFoot.Interact.performed += _ => interact.Interact();

        uiActions.OpenInventory.performed += _ =>  
        {
            inventoryUI.HandleInventory();
            OnFootSwitch();
        };
        // health debugging
        // onFoot.Jump.performed += ctx => health.TakeDamage(30);
        // onFoot.Reload.performed += ctx => health.Heal(30);
    }

    private void OnFootSwitch()
    {
        isUsingUI = !isUsingUI;
        if (isUsingUI)
        {
            onFoot.Disable();
        }
        else
        {
            onFoot.Enable();
        }
    }
}
