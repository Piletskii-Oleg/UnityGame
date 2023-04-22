using UnityEngine;

namespace Enemy.FlyingDragon.States
{
    public class LandingState : FlyingBaseState
    {
        private static readonly int landing = Animator.StringToHash("Land");
        
        private const float landingTime = 4f;
        private float timePassed;
        
        public LandingState(FlyingDragon dragon, BaseStateMachine stateMachine)
            : base(dragon, stateMachine)
        {
        }

        public override void Enter()
        {
            timePassed = 0;
            
            dragon.TriggerAnimation(landing);
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > landingTime)
            {
                stateMachine.ChangeState(dragon.StandState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}