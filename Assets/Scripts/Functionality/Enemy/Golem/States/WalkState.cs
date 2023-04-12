using Core.Enemy;

namespace Functionality.Enemy.Golem.States
{
    public class WalkState : GolemBaseState
    {
        public WalkState(Golem golem, BaseStateMachine stateMachine)
            : base(golem, stateMachine)
        {
        }

        public override void Enter()
        {
            golem.WalkToDestination(golem.FindPlayer());
        }

        public override void Tick()
        {
            if (golem.Agent.remainingDistance < golem.Agent.stoppingDistance)
            {
                stateMachine.ChangeState(golem.AttackState);
            }
        }

        public override void Exit()
        {
            golem.Stop();
        }
    }
}