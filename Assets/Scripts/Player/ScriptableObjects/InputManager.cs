using UnityEngine;

namespace Player.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Managers/Input Manager", order = 0)]
    public class InputManager : ScriptableObject
    {
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

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
        }

        public void EnableInput()
        {
            Cursor.lockState = CursorLockMode.Locked;
            inputController.EnableOnFoot();
        }
    }
}