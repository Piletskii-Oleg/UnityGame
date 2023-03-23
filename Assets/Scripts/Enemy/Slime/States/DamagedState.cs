using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime being damaged.
    /// </summary>
    public class DamagedState : SlimeBaseState
    {
        private static readonly int damageAnimationHash = Animator.StringToHash("Damage");
        private static readonly int damageType = Animator.StringToHash("DamageType");

        private float timePassed;
        private readonly float waitingTime;

        private readonly SlimeType slimeType;

        /// <summary>
        /// Initializes new instance of <see cref="DamagedState"/> class.
        /// </summary>
        /// <param name="slime">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        /// <param name="waitingTime">Amount of time which slime stays in damaged state for.</param>
        public DamagedState(Slime slime, BaseStateMachine stateMachine, Texture stateFace, float waitingTime)
            : base(slime, stateMachine, stateFace)
        {
            slimeType = slime.SlimeType;
            this.waitingTime = waitingTime;
        }
        
        public override void Enter()
        {
            base.Enter();

            timePassed = 0;

            slime.TriggerAnimation(damageAnimationHash);
            slime.SetAnimationValue(damageType, Random.Range(0, 3));

            if (slimeType is SlimeType.Neutral or SlimeType.Aggressive)
            {
                stateMachine.ChangeState(slime.AttackState);
            }
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > waitingTime)
            {
                stateMachine.ChangeState(slime.IdleState);
            }
        }

        public override void Exit()
        {
        }
    }
}