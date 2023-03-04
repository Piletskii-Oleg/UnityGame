using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime being damaged.
    /// </summary>
    public class DamagedState : BaseState
    {
        private static readonly int damageAnimationHash = Animator.StringToHash("Damage");
        private static readonly int damageType = Animator.StringToHash("DamageType");

        private float timePassed;
        private float animationTime = 1f;

        /// <summary>
        /// Initializes new instance of <see cref="DamagedState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
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