using UnityEngine;

namespace Enemy.Golem.States
{
    public class AttackState : GolemBaseState
    {
        private readonly int attackHash = Animator.StringToHash("Attack");
        private readonly int attackTypeHash = Animator.StringToHash("AttackType");

        public AttackState(Golem golem, BaseStateMachine stateMachine)
            : base(golem, stateMachine)
        {
        }
        
        public override void Enter()
        {
            golem.TriggerAnimation(attackHash);
            golem.SetAnimationValue(attackTypeHash, 1);
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            golem.SetAnimationValue(attackTypeHash, 0);
            
            
        }
    }
}