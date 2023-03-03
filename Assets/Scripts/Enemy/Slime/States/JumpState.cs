using UnityEngine;

namespace Enemy.Slime.States
{
    public class JumpState : BaseState
    {
        private static readonly int jumpAnimationHash = Animator.StringToHash("Jump");

        private bool isJumping;
        
        private void Jump()
        {
            actor.TriggerAnimation(jumpAnimationHash);
        }

        public JumpState(Slime actor, SlimeStateMachine stateMachine)
            : base(actor, stateMachine)
        {
        }

        public override void Enter()
        {
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