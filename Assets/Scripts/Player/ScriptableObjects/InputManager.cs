using UnityEngine;
using UnityEngine.Events;

namespace Player.ScriptableObjects
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

        public void Initialize()
            => inputController = playerScriptableObject
                .GetActualPlayerTransform()
                .gameObject
                .GetComponent<InputController>();

        public void LockInput()
        {
            Cursor.lockState = CursorLockMode.Confined;
            
            inputController.DisableOnFoot();
            
            onEnableUI.Invoke();
        }

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