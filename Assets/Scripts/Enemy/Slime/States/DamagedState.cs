using System.Linq;
using UnityEngine;

namespace Enemy.Slime.States
{
    public class DamagedState : BaseState
    {
        private static readonly int damageAnimationHash = Animator.StringToHash("Damage");
        private static readonly int damageType = Animator.StringToHash("DamageType");

        private float timePassed;
        private float animationTime = 1f;

        public DamagedState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
            : base(actor, stateMachine, stateFace)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            timePassed = 0;
            animationTime = Random.Range(0.3f, 0.7f);
            
            actor.TriggerAnimation(damageAnimationHash);
            actor.SetAnimationValue(damageType, Random.Range(0, 2));
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > animationTime)
            {
                stateMachine.ChangeState(actor.IdleState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}