using Shared;
using UnityEngine;

namespace Enemy.Spider
{
    public class SpiderAttackPlane : MonoBehaviour
    {
        [SerializeField] private Spider spider;
        
        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.gameObject.TryGetComponent<Actor>(out var actor))
            {
                spider.DealDamage(actor);
            }
        }
    }
}