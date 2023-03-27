using UnityEngine;

namespace Player.ScriptableObjects
{
    /// <summary>
    /// Stores information about player that can be accessed by any prefab.
    /// It receives information at runtime.
    /// </summary>
    [CreateAssetMenu( menuName = "PlayerSO")]
    public class PlayerScriptableObject : ScriptableObject
    {
        private GameObject player;
        private Transform playerTransform;

        public Vector3 GetActualPlayerPosition()
            => playerTransform.position;

        public Transform GetActualPlayerTransform()
            => playerTransform;

        public void Initialize()
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                playerTransform = player.transform;
            }
        }
    }
}