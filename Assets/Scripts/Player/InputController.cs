using UI;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// An input controller for a single-player game built using new Input System.
    /// </summary>
    public class InputController : MonoBehaviour
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
        [SerializeField] private PauseMenuUI pauseMenuUI;

        private bool isUsingUI;

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

            SubscribeToEventsOnFoot();
            SubscribeToEventsUIActions();
        }

        private void FixedUpdate()
        {
            movement.ProcessHorizontalMovement(onFoot.Movement.ReadValue<Vector2>());
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

        private void SubscribeToEventsOnFoot()
        {
            onFoot.Jump.performed += _ => movement.Jump();

            onFoot.Run.started += _ => movement.StartRunning();
            onFoot.Run.canceled += _ => movement.StopRunning();

            onFoot.Shoot.started += _ => weaponController.StartShooting();
            onFoot.Shoot.canceled += _ => weaponController.StopShooting();

            onFoot.Reload.performed += _ => weaponController.StartReload();

            onFoot.ChangeWeapon.performed +=
                _ => weaponController.ChangeWeapon((int)onFoot.ChangeWeapon.ReadValue<float>());

            onFoot.Zoom.started += _ => weaponZoom.Zoom(true);
            onFoot.Zoom.canceled += _ => weaponZoom.Zoom(false);

            onFoot.Interact.performed += _ => interact.Interact();
        }

        private void SubscribeToEventsUIActions()
        {
            uiActions.OpenInventory.performed += _ =>
            {
                inventoryUI.HandleInventory();
                OnFootSwitch();
            };
            
            uiActions.Pause.performed += _ =>
            {
                pauseMenuUI.HandlePauseMenu();
                OnFootSwitch();
            };
        }

        public void OnFootSwitch()
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
}
