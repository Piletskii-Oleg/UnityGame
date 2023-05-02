using Shared;
using UnityEngine;

namespace Enemy.Dragon
{
    public class DragonFire : MonoBehaviour
    {
        [SerializeField] private DragonFireSet fireSet;
        
        [SerializeField] private float fireStayTime;

        private float overallTimePassed;

        private void Update()
        {
            overallTimePassed += Time.deltaTime;

            if (overallTimePassed > fireStayTime)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Actor>(out var actor))
            {
                fireSet.StepInFire(actor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            fireSet.StepOutOfFire();
        }
    }
}