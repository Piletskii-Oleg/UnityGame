using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    private Health health;

   

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void InflictDamage(float damage)
    {
        if (health)
        {
            health.TakeDamage(damage);
            
        }
    }

    public void Heal(float healAmount)
    {
        if (health)
        {
            health.Heal(healAmount);
            
        }
    }
}
