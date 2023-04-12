using Core.Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Shared
{
    /// <summary>
    /// Class for a common enemy <see cref="Actor"/>
    /// whose current health should be independent of <see cref="HealthData"/>.
    /// </summary>
    public class CommonHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthData healthData;

        [SerializeField] private UnityEvent<float> onHealthChangedEvent; // Returns value from 0 to 1 which is the ratio of current health to max health.
        [SerializeField] private UnityEvent onDeathEvent;

        private float currentHealth;

        private void Start()
        {
            currentHealth = healthData.maxHealth;
        }

        /// <summary>
        /// Used when the <see cref="Actor"/> takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            onHealthChangedEvent.Invoke(currentHealth / healthData.maxHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                onDeathEvent.Invoke();
            }

            Debug.Log("Taken damage! " + gameObject.name + " - " + currentHealth);
        }

        /// <summary>
        /// Used when the <see cref="Actor"/> is healed.
        /// </summary>
        /// <param name="healAmount">Amount of health to recover.</param>
        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            if (currentHealth > healthData.maxHealth)
            {
                currentHealth = healthData.maxHealth;
            }

            onHealthChangedEvent.Invoke(currentHealth / healthData.maxHealth);
        }
    }
}
