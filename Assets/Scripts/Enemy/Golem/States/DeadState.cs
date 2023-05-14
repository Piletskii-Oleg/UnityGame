using UnityEngine;

namespace Enemy.Golem.States
{
    public class DeadState : GolemBaseState
    {
        private static readonly int death = Animator.StringToHash("Die");
        
        public DeadState(Golem golem, BaseStateMachine stateMachine)
            : base(golem, stateMachine)
        {
        }

        public override void Enter()
        {
            golem.TriggerAnimation(death);
            
            golem.Stop();
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}