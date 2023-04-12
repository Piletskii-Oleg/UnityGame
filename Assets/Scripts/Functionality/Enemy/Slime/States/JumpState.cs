using UnityEngine;

namespace Functionality.Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime jumping.
    /// </summary>
    public class JumpState : SlimeBaseState
    {
        private static readonly int jumpAnimationHash = Animator.StringToHash("Jump");

        private bool isJumping;

        /// <summary>
        /// Initializes new instance of <see cref="JumpState"/> class.
        /// </summary>
        /// <param name="slime">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        public JumpState(Slime slime, SlimeStateMachine stateMachine, Texture stateFace)
            : base(slime, stateMachine, stateFace)
        {
        }

        /// <summary>
        /// Makes the slime jump.
        /// </summary>
        private void Jump()
        {
            slime.TriggerAnimation(jumpAnimationHash);
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
            stateMachine.ChangeState(slime.IdleState);
        }
    }
}