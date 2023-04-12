using UnityEngine;

namespace Enemy.Golem.States
{
    public class DamagedState : GolemBaseState
    {
        private static readonly int damageAnimationHash = Animator.StringToHash("GetHit");
        
        public DamagedState(Golem golem, BaseStateMachine stateMachine)
            : base(golem, stateMachine)
        {
        }
        
        public override void Enter()
        {
            golem.TriggerAnimation(damageAnimationHash);

            stateMachine.ChangeState(golem.AttackState);
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}