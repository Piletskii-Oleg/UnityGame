using Shared;
using UnityEngine;

namespace Enemy.Spider
{
    /// <summary>
    /// Put on the plane that is used to deal damage by the spider.
    /// </summary>
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