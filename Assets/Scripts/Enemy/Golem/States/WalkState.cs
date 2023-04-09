using UnityEngine;

namespace Enemy.Golem.States
{
    public class WalkState : GolemBaseState
    {
        private readonly int doStep = Animator.StringToHash("doStep");

        public WalkState(Golem golem, BaseStateMachine stateMachine)
            : base(golem, stateMachine)
        {
        }

        public override void Enter()
        {
            golem.SetAnimationValue(doStep, true);
            
            MoveTowardsPlayer();
        }

        public override void Tick()
        {
            if (golem.Agent.remainingDistance < golem.Agent.stoppingDistance)
            {
                stateMachine.ChangeState(golem.AttackState);
            }
        }

        private void MoveTowardsPlayer()
        {
            golem.WalkToDestination(golem.FindPlayer());
        }

        public override void Exit()
        {
            golem.Stop();
        }
    }
}