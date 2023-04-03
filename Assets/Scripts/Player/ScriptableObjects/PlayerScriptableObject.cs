using UnityEngine;

namespace Player.ScriptableObjects
{
    /// <summary>
    /// Stores information about player that can be accessed by any prefab.
    /// It receives information at runtime.
    /// </summary>
    [CreateAssetMenu(menuName = "PlayerSO")]
    public class PlayerScriptableObject : ScriptableObject
    {
        private GameObject player;
        private Transform playerTransform;

        private Camera camera;

        public void Initialize()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerTransform = player.transform;

            camera = player.GetComponentInChildren<Camera>();
        }
        
        public Vector3 GetActualCameraPosition()
            => camera.transform.position;

        public Vector3 GetActualPlayerPosition()
            => playerTransform.position;

        public Transform GetActualPlayerTransform()
            => playerTransform;
    }
}