using Shared;
using UnityEngine;

namespace Enemy.Dragon.Fire
{
    [CreateAssetMenu(menuName = "Actor/Dragon/DragonFireSet", order = 0)]
    public class DragonFireSet : ScriptableObject
    {
        private int fireCount;
        private FireController fireController;

        /// <summary>
        /// Initializes the <see cref="DragonFireSet"/>. Required because <see cref="fireController"/>
        /// cannot be attached to a <see cref="ScriptableObject"/>.
        /// </summary>
        public void Initialize(FireController fireController)
        {
            fireCount = 0;
            this.fireController = fireController;
        }
        
        /// <summary>
        /// Called when the actor steps in fire.
        /// </summary>
        /// <param name="actor">Actor that steps in fire.</param>
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

        /// <summary>
        /// Called when the actor steps out of fire.
        /// </summary>
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