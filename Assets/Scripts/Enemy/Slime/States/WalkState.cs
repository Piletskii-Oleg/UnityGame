using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to slime walking towards a destination.
    /// </summary>
    public class WalkState : SlimeBaseState
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");

        private const float minIdleTime = 0.3f;
        private const float maxIdleTime = 5f;

        /// <summary>
        /// Initializes new instance of <see cref="WalkState"/> class.
        /// </summary>
        /// <param name="slime">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        public WalkState(Slime slime, BaseStateMachine stateMachine, Texture stateFace)
            : base(slime, stateMachine, stateFace)
        {
        }

        public override void Enter()
        {
            base.Enter();
            var destination = slime.GetNewPosition();
            slime.WalkToDestination(destination);
        }

        public override void Tick()
        {
            if (slime.SlimeType is SlimeType.Aggressive && slime.LookForPlayerInSegment())
            {
                stateMachine.ChangeState(slime.AttackState);
            }
            
            if (slime.Agent.remainingDistance < slime.Agent.stoppingDistance)
            {
                float timeLimit = Random.Range(minIdleTime, maxIdleTime);
                slime.IdleForPeriod(timeLimit, slime.IdleState, this);
            }
        }

        public override void Exit()
        {
            slime.SetAnimationValue(doStep, false);
        }
    }
}