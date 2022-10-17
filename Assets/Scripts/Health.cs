using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthData healthData;

    public void TakeDamage(float damage)
    {
        healthData.currentHealth -= damage;
        Debug.Log("Taken damage! " + gameObject.name + " - " + healthData.currentHealth);
    }

    public void Heal(float hp)
    {
        healthData.currentHealth += hp;
        if (healthData.currentHealth > healthData.maxHealth)
        {
            healthData.currentHealth = healthData.maxHealth;
        }
    }
}
