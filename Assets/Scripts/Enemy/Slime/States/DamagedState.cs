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
        private float waitingTime = 1f;

        private readonly SlimeType slimeType;

        /// <summary>
        /// Initializes new instance of <see cref="DamagedState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        public DamagedState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
            : base(actor, stateMachine, stateFace)
        {
            slimeType = actor.SlimeType;
        }
        
        public override void Enter()
        {
            base.Enter();

            timePassed = 0;
            waitingTime = Random.Range(0.3f, 0.7f);
            
            actor.TriggerAnimation(damageAnimationHash);
            actor.SetAnimationValue(damageType, Random.Range(0, 2));
        }

        public override void Tick()
        {
            actor.transform.LookAt(actor.LastHitPosition);
            switch (slimeType)
            {
                case SlimeType.Neutral:
                    stateMachine.ChangeState(actor.AttackState);
                    break;
                case SlimeType.Passive:
                {
                    timePassed += Time.deltaTime;
                    if (timePassed > waitingTime)
                    {
                        stateMachine.ChangeState(actor.IdleState);
                    }

                    break;
                }
            }
        }

        public override void Exit()
        {
            
        }
    }
}