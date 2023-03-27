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

        public Vector3 GetActualPlayerPosition()
        {
            return player.transform.position;
        }

        public Transform GetActualPlayerTransform()
        {
            return player.transform;
        }

        public void Initialize()
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}