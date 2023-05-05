using UnityEngine;

namespace Shared
{
    public class HealthPickup : MonoBehaviour
    {
        [Range(0, 100)]
        [SerializeField] private float healAmount;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<SingleHealth>(out var health))
            {
                if (health.gameObject.name != "Player")
                {
                    return;
                }
                
                health.Heal(healAmount);
                
                Destroy(gameObject);
            }
        }
    }
}