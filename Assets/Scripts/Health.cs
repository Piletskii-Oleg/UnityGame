using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthData healthData;

    private HealthUI healthUI;
    private Actor actor;

    private Coroutine healthUICoroutine;

    private void Start()
    {
        healthData.currentHealth = healthData.maxHealth;
        healthUI = GetComponent<HealthUI>();
        actor = GetComponent<Actor>();
    }

    public void TakeDamage(float damage)
    {
        healthData.currentHealth -= damage;
        if (healthData.currentHealth < 0)
        {
            healthData.currentHealth = 0;
            Kill();
        }

        if (healthUI)
        {
            UpdateUI();
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

        if (healthUI)
        {
            UpdateUI();
        }
    }

    public void Kill()
    {
        actor.OnKill();
    }

    private void UpdateUI()
    {
        if (healthUICoroutine != null)
        {
            StopCoroutine(healthUICoroutine);
        }

        healthUICoroutine = StartCoroutine(healthUI.UpdateHealthUI());
    }
}
