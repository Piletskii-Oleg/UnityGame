using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime jumping.
    /// </summary>
    public class JumpState : BaseState
    {
        private static readonly int jumpAnimationHash = Animator.StringToHash("Jump");

        private bool isJumping;

        /// <summary>
        /// Initializes new instance of <see cref="JumpState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        public JumpState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
            : base(actor, stateMachine, stateFace)
        {
        }

        /// <summary>
        /// Makes the slime jump.
        /// </summary>
        private void Jump()
        {
            actor.TriggerAnimation(jumpAnimationHash);
        }

        public override void Enter()
        {
            base.Enter();
            isJumping = true;
            Jump();
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            isJumping = false;
            stateMachine.ChangeState(actor.IdleState);
        }
    }
}