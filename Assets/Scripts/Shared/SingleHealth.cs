using UnityEngine;
using UnityEngine.Events;

namespace Shared
{
    /// <summary>
    /// Class for a unique enemy <see cref="Actor"/> whose health is dependent on <see cref="HealthData"/>.
    /// </summary>
    public class SingleHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HealthData healthData;

        [SerializeField] private UnityEvent onHealthChangedEvent;
        [SerializeField] private UnityEvent onDeathEvent;

        private void Start()
        {
            healthData.currentHealth = healthData.maxHealth;
        }

        /// <summary>
        /// Used when the <see cref="Actor"/> takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        public void TakeDamage(float damage)
        {
            healthData.currentHealth -= damage;
            onHealthChangedEvent.Invoke();

            if (healthData.currentHealth < 0)
            {
                healthData.currentHealth = 0;
                onDeathEvent.Invoke();
            }

            Debug.Log("Taken damage! " + gameObject.name + " - " + healthData.currentHealth);
        }

        /// <summary>
        /// Used when the <see cref="Actor"/> is healed.
        /// </summary>
        /// <param name="healAmount">Amount of health to recover.</param>
        public void Heal(float healAmount)
        {
            healthData.currentHealth += healAmount;
            if (healthData.currentHealth > healthData.maxHealth)
            {
                healthData.currentHealth = healthData.maxHealth;
            }

            onHealthChangedEvent.Invoke();
        }
    }
}
