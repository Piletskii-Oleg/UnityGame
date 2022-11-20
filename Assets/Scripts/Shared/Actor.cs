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
        [SerializeField] private UnityEvent<float> onTakeDamage;
        [SerializeField] private UnityEvent onKill;
        [SerializeField] private ActorAffiliation affiliation;

        /// <summary>
        /// Used when the actor takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        public void OnTakeDamage(float damage, ActorAffiliation affiliation)
        {
            if (this.affiliation != affiliation)
            {
                onTakeDamage.Invoke(damage);
            }
        }

        /// <summary>
        /// Used when the actor is killed.
        /// </summary>
        public void OnKill()
        {
            onKill.Invoke();
            Destroy(gameObject);
        }
    }
}
