using Shared;
using Shared.ScriptableObjects;
using UnityEngine;

namespace Enemy.Dragon
{
    public class DragonFire : MonoBehaviour
    {
        [SerializeField] private ActorData actorData;
        [SerializeField] private float damageCooldown;
        [SerializeField] private float fireStayTime;

        private float overallTimePassed;
        private float timePassed;
        
        private void Update()
        {
            timePassed += Time.deltaTime;
            overallTimePassed += Time.deltaTime;

            if (overallTimePassed > fireStayTime)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent<Actor>(out var actor))
            {
                if (timePassed > damageCooldown)
                {
                    actor.OnTakeDamage(actorData.damage, actorData.affiliation);

                    timePassed = 0;
                }
            }
        }
    }
}