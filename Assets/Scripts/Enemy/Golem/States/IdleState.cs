using UnityEngine;

namespace Enemy.Golem.States
{
    public class IdleState : GolemBaseState
    {
        private readonly float followTimeTact;
        private readonly bool hasBasePoint;
        
        private float timePassed;

        public IdleState(Golem golem, BaseStateMachine stateMachine, float followTimeTact, bool hasBasePoint)
            : base(golem, stateMachine)
        {
            this.hasBasePoint = hasBasePoint;
            this.followTimeTact = followTimeTact;
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > followTimeTact)
            {
                if (golem.LookForPlayerInSegment())
                {
                    stateMachine.ChangeState(golem.ChasePlayerState);
                }
                else if (hasBasePoint)
                {
                    stateMachine.ChangeState(golem.ReturnToBaseState);
                }
                
                timePassed = 0;
            }
        }

        public override void Exit()
        {
        }
    }
}