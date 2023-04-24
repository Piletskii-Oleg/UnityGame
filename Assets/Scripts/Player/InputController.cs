using System.Collections.Generic;
using System.Linq;
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

        private Dictionary<GameObject, int> openMenus;
        private int openMenusCount;

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

            openMenus = new Dictionary<GameObject, int>();
            openMenusCount = 0;

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
            uiActions.OpenInventory.performed += _ => inventoryUI.HandleInventory();
            uiActions.Pause.performed += _ => pauseMenuUI.HandlePauseMenu();
        }

        /// <summary>
        /// Attempts to enable onFoot input. Succeeds if no menus are open.
        /// </summary>
        /// <returns>True if enabling onFoot input succeeded, false otherwise.</returns>
        public bool TryEnableOnFoot(GameObject lockObject)
        {
            openMenus[lockObject] = 0;
            
            int count = OpenMenusCount();
            
            if (count == 0)
            {
                onFoot.Enable();
            }

            return count == 0;
        }

        /// <summary>
        /// Disables onFoot input.
        /// </summary>
        public void DisableOnFoot(GameObject lockObject)
        {
            openMenus[lockObject] = 1;
            onFoot.Disable();
        }

        private int OpenMenusCount()
            => (int)openMenus.Aggregate(0.0, (sum, keyValuePair) => sum + keyValuePair.Value);
    }
}
