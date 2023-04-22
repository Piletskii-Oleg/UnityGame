using UnityEngine;

namespace Enemy.FlyingDragon.States
{
    public class LandingState : FlyingBaseState
    {
        private static readonly int landing = Animator.StringToHash("Land");
        
        public LandingState(FlyingDragon dragon, BaseStateMachine stateMachine)
            : base(dragon, stateMachine)
        {
        }

        public override void Enter()
        {
            dragon.TriggerAnimation(landing);
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}