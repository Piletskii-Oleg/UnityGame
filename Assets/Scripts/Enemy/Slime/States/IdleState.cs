using System.Threading;
using UnityEngine;

namespace Enemy.Slime.States
{
    public class IdleState : BaseState
    {
        private static readonly int speedAnimationHash = Animator.StringToHash("Speed");
        private static readonly int doStep = Animator.StringToHash("DoStep");

        private float timePassed;
        private float timeLimit;
        
        private readonly float minIdleTime = 0.3f;
        private readonly float maxIdleTime = 5f;

        private bool isAggroed;
        
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
        
        private void Stop()
        {
            actor.SetAnimationValue(doStep, false);
            actor.Agent.isStopped = true;
        }
    }
}