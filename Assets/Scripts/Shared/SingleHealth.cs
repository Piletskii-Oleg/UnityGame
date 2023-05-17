using Shared.ScriptableObjects;
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

        [SerializeField] private UnityEvent<float> onHealthChangedEvent;
        [SerializeField] private UnityEvent onDeathEvent;

        private bool isDead;

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
            onHealthChangedEvent.Invoke(healthData.currentHealth / healthData.maxHealth);

            if (healthData.currentHealth <= 0)
            {
                healthData.currentHealth = 0;
                if (!isDead)
                {
                    isDead = true;
                    onDeathEvent.Invoke();
                }
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

            onHealthChangedEvent.Invoke(healthData.currentHealth / healthData.maxHealth);
        }
        
        /// <summary>
        /// Makes it possible to kill the actor again.
        /// </summary>
        public void Revive()
            => isDead = false;
    }
}
