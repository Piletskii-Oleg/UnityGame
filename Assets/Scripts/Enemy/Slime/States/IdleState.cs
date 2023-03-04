using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// Slime that corresponds to the slime being idle.
    /// </summary>
    public class IdleState : BaseState
    {
        private static readonly int speedAnimationHash = Animator.StringToHash("Speed");
        private static readonly int doStep = Animator.StringToHash("DoStep");

        private float timePassed;
        private float timeLimit;
        
        private readonly float minIdleTime = 0.3f;
        private readonly float maxIdleTime = 5f;

        private bool isAggroed;
        
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
            Stop();
            timeLimit = Random.Range(minIdleTime, maxIdleTime);
            timePassed = 0f;
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
        
        /// <summary>
        /// Stops the movement of the slime.
        /// </summary>
        private void Stop()
        {
            actor.SetAnimationValue(doStep, false);
            actor.Agent.isStopped = true;
        }
    }
}