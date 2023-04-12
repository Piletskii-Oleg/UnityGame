using UnityEngine;
using UnityEngine.Events;

namespace Core.Player.ScriptableObjects
{
    /// <summary>
    /// Manages locking and unlocking the player movement when opening menus.
    /// </summary>
    [CreateAssetMenu(menuName = "Managers/Input Manager", order = 0)]
    public class InputManager : ScriptableObject
    {
        [Header("Player Data")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Events")]
        [SerializeField] private UnityEvent onEnableUI;
        [SerializeField] private UnityEvent onDisableUI;

        private InputController inputController;

        /// <summary>
        /// Initializes the fields (used in <see cref="PlayerController"/> on awake).
        /// </summary>
        public void Initialize()
            => inputController = playerScriptableObject
                .GetActualPlayerTransform()
                .gameObject
                .GetComponent<InputController>();

        /// <summary>
        /// Locks the player movement and shows the cursor on screen.
        /// </summary>
        public void LockInput()
        {
            Cursor.lockState = CursorLockMode.Confined;
            
            inputController.DisableOnFoot();
            
            onEnableUI.Invoke();
        }

        /// <summary>
        /// If no menus are left open, unlocks the player movement and hides the cursor from the screen.
        /// </summary>
        public void EnableInput()
        {
            if (inputController.TryEnableOnFoot())
            {
                Cursor.lockState = CursorLockMode.Locked;
                
                onDisableUI.Invoke();
            }
        }
    }
}