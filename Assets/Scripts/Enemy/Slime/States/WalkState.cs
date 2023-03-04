using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to slime walking towards a destination.
    /// </summary>
    public class WalkState : BaseState
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        
        private SlimeArea slimeArea;
        private Vector3 destination;

        /// <summary>
        /// Initializes new instance of <see cref="WalkState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        /// <param name="slimeArea">Area in which slime can walk.</param>
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
        
        /// <summary>
        /// Activates the walk animation and makes slime move towards its next destination.
        /// </summary>
        private void StartWalking()
        {
            actor.SetAnimationValue(doStep, true);
            actor.Agent.SetDestination(destination);
            actor.Agent.isStopped = false;
        }
    }
}