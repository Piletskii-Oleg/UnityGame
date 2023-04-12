using UnityEngine;

namespace Functionality.Enemy.Golem.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/Golem/Stone Data")]
    public class GolemStoneData : ScriptableObject
    {
        [Tooltip("Minimal speed required for the stone to deal damage")]
        public float minimalVelocity;

        [Tooltip("Time in seconds until the stone disappears after it hits something")]
        public float timeUntilDisappearing;
    }
}