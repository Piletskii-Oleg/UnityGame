using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// Slime that corresponds to the slime being idle.
    /// </summary>
    public class IdleState : BaseState
    {
        private float timePassed;
        private float timeLimit;

        /// <summary>
        /// Initializes new instance of <see cref="IdleState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        public IdleState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
            : base(actor, stateMachine, stateFace)
        {
        }

        public override void Enter()
        {
            base.Enter();
            actor.Stop();

            timeLimit = Random.Range(0.6f, 1.3f); // min value should be less than period in actor.IdleForPeriod(float period,...)
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > timeLimit)
            {
                stateMachine.ChangeState(actor.WalkState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}