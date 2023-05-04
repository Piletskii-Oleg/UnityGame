using UnityEngine;

namespace Shared.ScriptableObjects
{
    /// <summary>
    /// Data for health of an entity in the world.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/Health Data")]
    public class HealthData : ScriptableObject
    {
        [Header("Health parameters")]
        public float maxHealth;
        public float currentHealth;
    }
}
