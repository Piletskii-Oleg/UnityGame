using System.Collections.Generic;
using UnityEngine;

namespace Shared.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Actor Affiliation")]
    public class ActorAffiliation : ScriptableObject
    {
        [Tooltip("Actors that are friendly to this actor.")]
        public List<ActorAffiliation> friendlyFractions;

        [Tooltip("Actors that are hostile to this actor.")]
        public List<ActorAffiliation> enemyFractions;

        [Tooltip("Actors that are neutral to this actor.")]
        public List<ActorAffiliation> neutralFractions;
    }
}