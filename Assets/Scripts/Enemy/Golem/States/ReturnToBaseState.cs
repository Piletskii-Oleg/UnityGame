using UnityEngine;

namespace Enemy.Golem.States
{
    public class ReturnToBaseState : GolemBaseState
    {
        private readonly Vector3 basePosition;
        private readonly Transform golemTransform;

        private const float basePointStoppingDistance = 2.0f;
        private readonly float agentStoppingDistance;
        
        public ReturnToBaseState(Golem golem, BaseStateMachine stateMachine, Transform basePoint)
            : base(golem, stateMachine)
        {
            golemTransform = golem.transform;
            
            basePosition = basePoint.position;
            basePosition.y = golemTransform.position.y;
            
            agentStoppingDistance = golem.Agent.stoppingDistance;
        }

        public override void Enter()
        {
            if (IsFarFromBase())
            {
                golem.WalkToDestination(basePosition);
                golem.Agent.stoppingDistance = basePointStoppingDistance;
            }
        }

        public override void Tick()
        {
            if (golem.Agent.remainingDistance < golem.Agent.stoppingDistance)
            {
                golem.TurnToBaseAngle();
                stateMachine.ChangeState(golem.IdleState);
            }
        }

        public override void Exit()
        {
            golem.Agent.stoppingDistance = agentStoppingDistance;
            golem.Stop();
        }
        
        private bool IsFarFromBase()
            => Vector3.Distance(golemTransform.position, basePosition) > basePointStoppingDistance;
    }
}