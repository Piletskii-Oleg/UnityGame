using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class BossArea : CircleArea
    {
        private SphereCollider sphereCollider;
        
        [SerializeField] private UnityEvent onAreaEnter;
        [SerializeField] private UnityEvent onAreaExit;

        private void Start()
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = enemyRoamRadius;
            sphereCollider.isTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                onAreaExit.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                onAreaEnter.Invoke();
            }
        }
    }
}