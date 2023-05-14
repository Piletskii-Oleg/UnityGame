using Shared;
using UnityEngine;

namespace Enemy.Dragon.Fire
{
    [CreateAssetMenu(menuName = "Actor/Dragon/DragonFireSet", order = 0)]
    public class DragonFireSet : ScriptableObject
    {
        private int fireCount;
        private FireController fireController;

        public void Initialize(FireController fireController)
        {
            fireCount = 0;
            this.fireController = fireController;
        }
        
        public void StepInFire(Actor actor)
        {
            if (actor.Name == "Dragon")
            {
                return;
            }
            
            fireCount++;

            if (fireCount == 1)
            {
                fireController.StartDealingFireDamage(actor);
            }
        }

        public void StepOutOfFire()
        {
            fireCount--;

            if (fireCount == 0)
            {
                fireController.StopDealingFireDamage();
            }
        }
    }
}