using UnityEngine;

namespace Enemy.Slime.States
{
    public class WalkState : BaseState
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        
        private SlimeArea slimeArea;
        private Vector3 destination;

        public WalkState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace, SlimeArea slimeArea)
            : base(actor, stateMachine, stateFace)
        {
            this.slimeArea = slimeArea;
        }

        public override void Enter()
        {
            base.Enter();
            destination = slimeArea.GetNewPosition();
            StartWalking();
        }

        public override void Tick()
        {
            if (actor.Agent.remainingDistance < actor.Agent.stoppingDistance)
            {
                actor.Animator.SetBool(doStep, false);
                stateMachine.ChangeState(actor.IdleState);
            }
        }

        public override void Exit()
        {
            actor.SetAnimationValue(doStep, false);
        }
        
        private void StartWalking()
        {
            actor.SetAnimationValue(doStep, true);
            actor.Agent.SetDestination(destination);
            actor.Agent.isStopped = false;
        }
    }
}