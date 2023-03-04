using UnityEngine;

namespace Enemy.Slime.States
{
    public class JumpState : BaseState
    {
        private static readonly int jumpAnimationHash = Animator.StringToHash("Jump");

        private bool isJumping;

        public JumpState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
            : base(actor, stateMachine, stateFace)
        {
        }

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