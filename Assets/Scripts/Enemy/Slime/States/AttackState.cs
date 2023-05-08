using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime attacking the player.
    /// </summary>
    public class AttackState : SlimeBaseState
    {
        private static readonly int attack = Animator.StringToHash("Attack");
        
        private float timePassed;
        private readonly float followTimeTact;

        private int timesPlayerIsNotFound;
        private readonly int timesPlayerIsSearched;

        /// <summary>
        /// Initializes new instance of <see cref="AttackState"/> class.
        /// </summary>
        /// <param name="slime">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        /// <param name="followTimeTact">Time that should pass until slime looks for the player again.</param>
        /// <param name="timesPlayerIsSearched">Amount of times that slime will try to look for a player.</param>
        public AttackState(Slime slime, BaseStateMachine stateMachine, Texture stateFace, float followTimeTact, int timesPlayerIsSearched)
            : base(slime, stateMachine, stateFace)
        {
            this.followTimeTact = followTimeTact;
            this.timesPlayerIsSearched = timesPlayerIsSearched;
        }

        public override void Enter()
        {
            base.Enter();
            
            timesPlayerIsNotFound = 0;
            
            UpdateTact();
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed >= followTimeTact)
            {
                timePassed = 0f;
                UpdateTact();
            }

            if (slime.Agent.remainingDistance < slime.Agent.stoppingDistance)
            {
                Attack();
                slime.Stop();
                timePassed = followTimeTact;
            }
        }

        /// <summary>
        /// Forces recalculation of player's position and the consequent behavior of the slime.
        /// </summary>
        private void UpdateTact()
        {
            if (slime.LookForPlayer())
            {
                slime.WalkToDestination(slime.PlayerPosition);
            }
            else
            {
                timesPlayerIsNotFound++;
            }

            if (timesPlayerIsNotFound > timesPlayerIsSearched)
            {
                slime.IdleForPeriod(0.3f, slime.IdleState, slime.WalkState);
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
            slime.TriggerAnimation(attack);
        }
    }
}