using UnityEngine;

namespace Enemy.Spider.States
{
    public class WalkState : SpiderBaseState
    {
        public WalkState(Spider spider, BaseStateMachine stateMachine)
            : base(spider, stateMachine)
        {
        }

        private static readonly int doStep = Animator.StringToHash("DoStep");

        private readonly float minIdleTime = 0.3f;
        private readonly float maxIdleTime = 5f;

        public override void Enter()
        {
            // var destination = slimeArea.GetNewPosition();
            // slime.WalkToDestination(destination);
        }

        public override void Tick()
        {
            if (spider.LookForPlayerInSegment())
            {
                stateMachine.ChangeState(spider.AttackState);
            }
            
            if (spider.Agent.remainingDistance < spider.Agent.stoppingDistance)
            {
                float timeLimit = Random.Range(minIdleTime, maxIdleTime);
                spider.IdleForPeriod(timeLimit, spider.IdleState, this);
            }
        }

        public override void Exit()
        {
            spider.SetAnimationValue(doStep, false);
        }
    }
}