using Core.Enemy;
using UnityEngine;

namespace Functionality.Enemy.Spider.States
{
    /// <summary>
    /// Slime that corresponds to the spider walking.
    /// </summary>
    public class WalkState : SpiderBaseState
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");

        private readonly float minIdleTime = 0.3f;
        private readonly float maxIdleTime = 5f;
        
        public WalkState(Spider spider, BaseStateMachine stateMachine)
            : base(spider, stateMachine)
        {
        }

        public override void Enter()
        {
            var destination = spider.GetNewPositionInArea();
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