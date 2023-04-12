using Core.Enemy;
using UnityEngine;

namespace Functionality.Enemy.Slime.States
{
    public class DeadState : SlimeBaseState
    {
        private static readonly int damageAnimationHash = Animator.StringToHash("Damage");
        private static readonly int damageType = Animator.StringToHash("DamageType");
        
        public DeadState(Slime slime, BaseStateMachine stateMachine, Texture stateFace)
            : base(slime, stateMachine, stateFace)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            slime.Stop();
                        
            slime.TriggerAnimation(damageAnimationHash);
            slime.SetAnimationValue(damageType, 2);
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}