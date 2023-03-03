using System.Threading;
using UnityEngine;

namespace Enemy.Slime.States
{
    public class IdleState : BaseState
    {
        private static readonly int speedAnimationHash = Animator.StringToHash("Speed");

        private float timePassed;
        private float timeLimit;
        
        private float minIdleTime = 0.3f;
        private float maxIdleTime = 5f;
        
        public IdleState(Slime actor, SlimeStateMachine stateMachine)
            : base(actor, stateMachine)
        {
        }

        public override void Enter()
        {
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
            actor.StopAnimation(speedAnimationHash);
            //actor.Agent.isStopped = true;
        }
    }
}