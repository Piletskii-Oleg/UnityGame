using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthData healthData;
    private float currentHealth;

    [SerializeField] private UnityEvent onHealthChangedEvent;
    [SerializeField] private UnityEvent onDeathEvent;

    private void Start()
    {
        currentHealth = healthData.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        onHealthChangedEvent.Invoke();

        if (currentHealth < 0)
        {
            currentHealth = 0;
            onDeathEvent.Invoke();
        }

        Debug.Log("Taken damage! " + gameObject.name + " - " + currentHealth);
    }

    public void Heal(float hp)
    {
        currentHealth += hp;
        if (currentHealth > healthData.maxHealth)
        {
            currentHealth = healthData.maxHealth;
        }

        onHealthChangedEvent.Invoke();
    }
}
