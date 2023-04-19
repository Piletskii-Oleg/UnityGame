using Player.ScriptableObjects;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace NPC
{
    public class DragonNpcActor : Actor
    {
        [SerializeField] private UnityEvent onDamaged;
        [SerializeField] private PlayerScriptableObject player;

        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            if (!actorData.affiliation.friendlyFractions.Contains(actorAffiliation))
            {
                return;
            }
            
            var playerPosition = player.GetActualPlayerPosition();
            if (Vector3.Distance(playerPosition, transform.position) < 40f)
            {
                onDamaged.Invoke();
            }
        }
    }
}