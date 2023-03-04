using UnityEngine;

namespace Enemy.Slime.States
{
    public class WalkState : BaseState
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        
        private int currentWaypointIndex;
        private Transform[] waypoints;

        public WalkState(Slime actor, SlimeStateMachine stateMachine, Transform[] waypoints)
            : base(actor, stateMachine)
        {
            this.waypoints = waypoints;
            currentWaypointIndex = 0;
        }

        public override void Enter()
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            StartWalking();
        }

        public override void Tick()
        {
            if (actor.Agent.remainingDistance < actor.Agent.stoppingDistance)
            {
                actor.Animator.SetBool(doStep, false);
                stateMachine.ChangeState(actor.IdleState);
            }
        }

        public override void Exit()
        {
            actor.SetAnimationValue(doStep, false);
        }
        
        private void StartWalking()
        {
            actor.SetAnimationValue(doStep, true);
            actor.Agent.SetDestination(waypoints[currentWaypointIndex].position);
            actor.Agent.isStopped = false;
        }
    }
}