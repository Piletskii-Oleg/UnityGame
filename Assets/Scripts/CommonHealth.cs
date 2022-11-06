using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class for a common enemy <see cref="Actor"/> whose health should independent of <see cref="HealthData"/>.
/// </summary>
public class CommonHealth : MonoBehaviour
{
    [SerializeField] private HealthData healthData;

    [SerializeField] private UnityEvent<float> onHealthChangedEvent; // Returns value from 0 to 1 which is the ratio of current health to max health.
    [SerializeField] private UnityEvent onDeathEvent;

    public float CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = healthData.maxHealth;
    }

    /// <summary>
    /// Used when the <see cref="Actor"/> takes damage.
    /// </summary>
    /// <param name="damage">Damage taken.</param>
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        onHealthChangedEvent.Invoke(CurrentHealth / healthData.maxHealth);

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            onDeathEvent.Invoke();
        }

        Debug.Log("Taken damage! " + gameObject.name + " - " + CurrentHealth);
    }

    /// <summary>
    /// Used when the <see cref="Actor"/> is healed.
    /// </summary>
    /// <param name="healAmount">Amount of health to recover.</param>
    public void Heal(float hp)
    {
        CurrentHealth += hp;
        if (CurrentHealth > healthData.maxHealth)
        {
            CurrentHealth = healthData.maxHealth;
        }

        onHealthChangedEvent.Invoke(CurrentHealth / healthData.maxHealth);
    }
}
