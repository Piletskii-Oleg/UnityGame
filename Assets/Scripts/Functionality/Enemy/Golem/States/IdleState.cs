using Core.Enemy;
using UnityEngine;

namespace Functionality.Enemy.Golem.States
{
    public class IdleState : GolemBaseState
    {
        private float timePassed;
        private readonly float followTimeTact;
        
        public IdleState(Golem golem, BaseStateMachine stateMachine, float followTimeTact)
            : base(golem, stateMachine)
        {
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
                    stateMachine.ChangeState(golem.WalkState);
                }

                timePassed = 0;
            }
        }

        public override void Exit()
        {
        }
    }
}