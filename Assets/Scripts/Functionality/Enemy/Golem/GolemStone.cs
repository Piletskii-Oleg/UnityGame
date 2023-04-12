using System.Collections;
using Core.Shared;
using Core.Shared.ScriptableObjects;
using Functionality.Enemy.Golem.ScriptableObjects;
using UnityEngine;

namespace Functionality.Enemy.Golem
{
    public class GolemStone : MonoBehaviour
    {
        [SerializeField] private GolemStoneData stoneData;

        private Rigidbody rigidBody;
        
        private float maxForce;
        
        private ActorAffiliation golemAffiliation;
        private float stoneDamage;

        private bool hasHit;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Initializes values that are received from Golem.
        /// </summary>
        /// <param name="damage">Damage dealt by the stone.</param>
        /// <param name="affiliation">Affiliation of the golem.</param>
        /// <param name="force">Initial force with which golem throws the stone.</param>
        public void Initialize(float damage, ActorAffiliation affiliation, float force)
        {
            stoneDamage = damage;
            golemAffiliation = affiliation;
            maxForce = force;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Actor>(out var actor))
            {
                TryDealDamage(actor);
            }
            else if (!hasHit)
            {
                StartCoroutine(Disappear());
            }
            
            hasHit = true;
        }

        private void TryDealDamage(Actor actor)
        {
            if (rigidBody.velocity.magnitude > stoneData.minimalVelocity)
            {
                float actualDamage = rigidBody.velocity.magnitude / maxForce * stoneDamage;
                actor.OnTakeDamage(actualDamage, golemAffiliation);
            }
        }
        
        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(stoneData.timeUntilDisappearing);

            var stone = gameObject;
            stone.SetActive(false);
            Destroy(stone);
        }
    }
}