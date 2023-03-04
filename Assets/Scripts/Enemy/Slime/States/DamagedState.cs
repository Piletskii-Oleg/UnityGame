using UnityEngine;

namespace Enemy.Slime.States
{
    public class DamagedState : BaseState
    {
        private static readonly int damageAnimationHash = Animator.StringToHash("Damage");
        private static readonly int damageType = Animator.StringToHash("DamageType");

        public DamagedState(Slime actor, SlimeStateMachine stateMachine)
            : base(actor, stateMachine)
        {
        }

        public override void Enter()
        {
            actor.TriggerAnimation(damageAnimationHash);
            actor.SetAnimationValue(damageType, Random.Range(0, 2));
        }

        public override void Tick()
        {
            stateMachine.ChangeState(actor.IdleState);
        }

        public override void Exit()
        {
            
        }
    }
}