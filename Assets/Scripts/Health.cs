using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthData healthData;

    [SerializeField] private UnityEvent onHealthChangedEvent;
    [SerializeField] private UnityEvent onDeathEvent;

    private void Start()
    {
        healthData.currentHealth = healthData.maxHealth;
    }

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

    public void Heal(float hp)
    {
        healthData.currentHealth += hp;
        if (healthData.currentHealth > healthData.maxHealth)
        {
            healthData.currentHealth = healthData.maxHealth;
        }

        onHealthChangedEvent.Invoke();
    }
}
