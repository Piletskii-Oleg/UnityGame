﻿using UnityEngine;

namespace Enemy.Slime.States
{
    public class WalkState : BaseState
    {
        private static readonly int speedAnimationHash = Animator.StringToHash("Speed");

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
            Walk();
        }

        public override void Tick()
        {
            if (actor.Agent.remainingDistance < actor.Agent.stoppingDistance)
            {
                stateMachine.ChangeState(actor.IdleState);
            }
        }

        public override void Exit()
        {
            
        }
        
        private void Walk()
        {
            actor.Agent.SetDestination(waypoints[currentWaypointIndex].position);
            actor.Agent.isStopped = false;
            actor.SetAnimationValue(speedAnimationHash, 3);
        }
    }
}