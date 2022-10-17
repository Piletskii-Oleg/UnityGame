using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
