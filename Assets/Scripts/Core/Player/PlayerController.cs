using Core.Player.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    /// <summary>
    /// Used to initialize player scriptable objects state.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [SerializeField] private InputManager inputManager;

        private void Awake()
        {
            playerScriptableObject.Initialize();
            inputManager.Initialize();
        }
    }
}