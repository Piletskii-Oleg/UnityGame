using UnityEngine;

namespace Enemy.FlyingDragon.States
{
    public class StandState : FlyingBaseState
    {
        private const float idleTime = 3.2f;
        private float timePassed;
        
        public StandState(FlyingDragon dragon, BaseStateMachine stateMachine)
            : base(dragon, stateMachine)
        {
        }

        public override void Enter()
        {
            timePassed = 0;
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > idleTime)
            {
                timePassed = 0;
                
                stateMachine.ChangeState(dragon.TakeOffState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}