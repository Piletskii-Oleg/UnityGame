using Shared.ScriptableObjects;
using UnityEngine;

namespace Shared
{
    public class HealthSurrogate : MonoBehaviour
    {
        [SerializeField] private Actor actor;

        public void OnTakeDamage(float damage, ActorAffiliation affiliation)
            => actor.OnTakeDamage(damage, affiliation);
    }
}