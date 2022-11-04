using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Health of an <see cref="Actor"/>.
/// </summary>
public class Health : MonoBehaviour
{
    [Tooltip("Scriptable object that stores health data.")]
    [SerializeField] private HealthData healthData;

    [SerializeField] private UnityEvent onHealthChangedEvent;
    [SerializeField] private UnityEvent onDeathEvent;

    private void Start()
    {
        healthData.currentHealth = healthData.maxHealth;
    }

    /// <summary>
    /// Used when the actor takes damage.
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
