using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour // abstract
{
    [SerializeField] private UnityEvent<float> onTakeDamage;
    [SerializeField] private UnityEvent onKill;

    public void OnTakeDamage(float damage)
    {
        onTakeDamage.Invoke(damage);
    }

    public void OnKill()
    {
        Destroy(gameObject);
    }
}
