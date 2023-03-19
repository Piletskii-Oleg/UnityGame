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
        private readonly float waitingTime;

        private readonly SlimeType slimeType;

        private readonly Transform transformToFollow; // delete?

        /// <summary>
        /// Initializes new instance of <see cref="DamagedState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        /// <param name="waitingTime">Amount of time which slime stays in damaged state for.</param>
        /// <param name="transformToFollow">Transform to follow them if attacked.</param>
        public DamagedState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace, float waitingTime, Transform transformToFollow)
            : base(actor, stateMachine, stateFace)
        {
            slimeType = actor.SlimeType;
            this.waitingTime = waitingTime;
            this.transformToFollow = transformToFollow;
        }
        
        public override void Enter()
        {
            base.Enter();

            timePassed = 0;

            actor.TriggerAnimation(damageAnimationHash);
            actor.SetAnimationValue(damageType, Random.Range(0, 2));

            if (slimeType is SlimeType.Neutral or SlimeType.Aggressive)
            {
                stateMachine.ChangeState(actor.AttackState);
            }
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > waitingTime)
            {
                stateMachine.ChangeState(actor.IdleState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}