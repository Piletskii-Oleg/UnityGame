using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class BossArea : CircleAreaBase
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
                Debug.Log("exit");
                onAreaExit.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("enter");
                onAreaEnter.Invoke();
            }
        }
    }
}