using Shared.ScriptableObjects;
using UnityEngine;

namespace Shared
{
    [CreateAssetMenu(menuName = "Data/Actor Data", order = 0)]
    public class ActorData : ScriptableObject
    {
        [Tooltip("Damage dealt by that enemy")]
        public float damage;

        public ActorAffiliation affiliation;
    }
}