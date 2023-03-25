using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Shared
{
    /// <summary>
    /// Basic class for an actor (player, enemy etc.) in the world.
    /// </summary>
    public class Actor : MonoBehaviour // abstract
    {
        [Header("Events")]
        [SerializeField] private UnityEvent<float> onTakeDamage;
        [SerializeField] private UnityEvent onKill;
        
        [Header("Affiliation")]
        [SerializeField] protected ActorAffiliation affiliation;

        /// <summary>
        /// Used when the actor takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        /// <param name="actorAffiliation">Affiliation of the actor that tries to inflict damage.</param>
        public virtual void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            if (this.affiliation.enemyFractions.Contains(actorAffiliation))
            {
                onTakeDamage.Invoke(damage);
            }
        }

        /// <summary>
        /// Used when the actor takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        /// <param name="actorAffiliation">Affiliation of the actor that tries to inflict damage.</param>
        /// <param name="hitPosition">Position where actor was hit by another actor.</param>
        public virtual void OnTakeDamage(float damage, ActorAffiliation actorAffiliation, Vector3 hitPosition)
        {
            if (this.affiliation.enemyFractions.Contains(actorAffiliation))
            {
                onTakeDamage.Invoke(damage);
            }
        }

        /// <summary>
        /// Used when the actor is killed.
        /// </summary>
        public virtual void OnKill()
        {
            onKill.Invoke();
        }
    }
}
