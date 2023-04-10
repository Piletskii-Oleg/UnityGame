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
        
        [Header("Data")]
        [SerializeField] protected ActorData actorData;

        /// <summary>
        /// Used when the actor takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        /// <param name="actorAffiliation">Affiliation of the actor that tries to inflict damage.</param>
        public virtual void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            if (actorData.affiliation.enemyFractions.Contains(actorAffiliation))
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
