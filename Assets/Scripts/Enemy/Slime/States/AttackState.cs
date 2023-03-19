using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime attacking the player.
    /// </summary>
    public class AttackState : BaseState
    {
        private static readonly int attack = Animator.StringToHash("Attack");
        
        private float timePassed;
        private readonly float followTimeTact;

        private int timesPlayerIsNotFound;
        private readonly int timesPlayerIsSearched;

        /// <summary>
        /// Initializes new instance of <see cref="AttackState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        /// <param name="lookRadius">Radius of a circle in which slime will look for the player.</param>
        /// <param name="followTimeTact">Time that should pass until slime looks for the player again.</param>
        /// <param name="timesPlayerIsSearched">Amount of times that slime will try to look for a player.</param>
        public AttackState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace, float followTimeTact, int timesPlayerIsSearched)
            : base(actor, stateMachine, stateFace)
        {
            this.followTimeTact = followTimeTact;
            this.timesPlayerIsSearched = timesPlayerIsSearched;
        }

        public override void Enter()
        {
            base.Enter();
            timesPlayerIsNotFound = 0;
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed >= followTimeTact)
            {
                timePassed = 0f;
                UpdateTact();
            }

            if (actor.Agent.remainingDistance < actor.Agent.stoppingDistance)
            {
                Attack();
                actor.Stop();
                timePassed = followTimeTact;
            }
        }

        /// <summary>
        /// Forces recalculation of player's position and the consequent behavior of the slime.
        /// </summary>
        private void UpdateTact()
        {
            if (actor.LookForPlayer())
            {
                actor.WalkToDestination(actor.PlayerPosition);
            }
            else
            {
                timesPlayerIsNotFound++;
            }

            if (timesPlayerIsNotFound > timesPlayerIsSearched)
            {
                actor.IdleForPeriod(0.3f, actor.IdleState, actor.WalkState);
            }
        }

        public override void Exit()
        {
        }

        /// <summary>
        /// Makes slime attack the player.
        /// </summary>
        private void Attack()
        {
            actor.TriggerAnimation(attack);
        }
    }
}