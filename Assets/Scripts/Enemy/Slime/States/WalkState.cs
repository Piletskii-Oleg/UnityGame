using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to slime walking towards a destination.
    /// </summary>
    public class WalkState : BaseState
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        
        private readonly SlimeArea slimeArea;

        private readonly float minIdleTime = 0.3f;
        private readonly float maxIdleTime = 5f;

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
            var destination = slimeArea.GetNewPosition();
            actor.WalkToDestination(destination);
        }

        public override void Tick()
        {
            if (actor.Agent.remainingDistance < actor.Agent.stoppingDistance)
            {
                float timeLimit = Random.Range(minIdleTime, maxIdleTime);
                actor.IdleForPeriod(timeLimit, actor.IdleState, this);
            }
        }

        public override void Exit()
        {
            actor.SetAnimationValue(doStep, false);
        }
    }
}