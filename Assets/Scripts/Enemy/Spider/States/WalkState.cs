using UnityEngine;

namespace Enemy.Spider.States
{
    public class WalkState : SpiderBaseState
    {
        private readonly CircleArea area;
        
        public WalkState(Spider spider, BaseStateMachine stateMachine, CircleArea area)
            : base(spider, stateMachine)
        {
            this.area = area;
        }

        private static readonly int doStep = Animator.StringToHash("DoStep");

        private readonly float minIdleTime = 0.3f;
        private readonly float maxIdleTime = 5f;

        public override void Enter()
        {
            var destination = area.GetNewPosition();
            spider.WalkToDestination(destination);
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