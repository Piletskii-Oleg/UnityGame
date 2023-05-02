using Shared;
using UnityEngine;

namespace Enemy.Dragon
{
    [CreateAssetMenu(menuName = "Actor/Dragon/DragonFireSet", order = 0)]
    public class DragonFireSet : ScriptableObject
    {
        private int fireCount;
        private Dragon dragon;

        public void Initialize(Dragon dragon)
        {
            fireCount = 0;
            this.dragon = dragon;
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
                dragon.StartDealingFireDamage(actor);
            }
        }

        public void StepOutOfFire()
        {
            fireCount--;

            if (fireCount == 0)
            {
                dragon.StopDealingFireDamage();
            }
        }
    }
}