using Shared;
using UnityEngine;

namespace Player.ScriptableObjects
{
    /// <summary>
    /// Stores information about player that can be accessed by any prefab.
    /// It receives information at runtime in Awake method of <see cref="PlayerController"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "PlayerSO")]
    public class PlayerScriptableObject : ScriptableObject
    {
        private GameObject player;
        private Transform playerTransform;

        private Camera camera;

        /// <summary>
        /// Initializes the fields (used in <see cref="PlayerController"/> on awake).
        /// </summary>
        public void Initialize()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerTransform = player.transform;

            camera = player.GetComponentInChildren<Camera>();
            PlayerRigidbody = player.GetComponent<Rigidbody>();
            PlayerActor = player.GetComponent<Actor>();
        }
        
        /// <summary>
        /// Gets the actual position of the player's camera.
        /// </summary>
        public Vector3 GetActualCameraPosition()
            => camera.transform.position;

        public Rigidbody PlayerRigidbody { get; private set; }

        public Actor PlayerActor { get; private set; }

        /// <summary>
        /// Gets the actual position of the player.
        /// </summary>
        public Vector3 GetActualPlayerPosition()
            => playerTransform.position;

        /// <summary>
        /// Gets the actual transform of the player.
        /// </summary>
        public Transform GetActualPlayerTransform()
            => playerTransform;
    }
}