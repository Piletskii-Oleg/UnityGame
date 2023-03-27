using Player.ScriptableObjects;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Used to initialize player scriptable object state.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        private void Awake()
        {
            playerScriptableObject.Initialize();
        }
    }
}