using UnityEngine;

namespace Enemy.Spider.States
{
    public class SpawnedState : SpiderBaseState
    {
        private readonly Vector3 exitPosition;
        
        public SpawnedState(Spider spider, BaseStateMachine stateMachine, Vector3 exitPosition)
            : base(spider, stateMachine)
        {
            this.exitPosition = exitPosition;
        }

        public override void Enter()
        {
            spider.WalkToDestination(exitPosition);
        }

        public override void Tick()
        {
            if (spider.Agent.remainingDistance < spider.Agent.stoppingDistance)
            {
                stateMachine.ChangeState(spider.IdleState);
            }
        }

        public override void Exit()
        {
        }
    }
}