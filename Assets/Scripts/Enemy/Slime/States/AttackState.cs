using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime attacking the player.
    /// </summary>
    public class AttackState : BaseState
    {
        private static readonly int attack = Animator.StringToHash("Attack");

        private float lookRadius;
        private readonly LayerMask playerMask = 1 << LayerMask.NameToLayer("Player");
        private readonly Collider[] playerInRange = new Collider[1];
        private Vector3 playerPosition;

        private float timePassed;
        private float followTimeTact;

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
        public AttackState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace, float lookRadius, float followTimeTact, int timesPlayerIsSearched)
            : base(actor, stateMachine, stateFace)
        {
            this.lookRadius = lookRadius;
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

        private void UpdateTact()
        {
            if (LookForPlayer())
            {
                actor.WalkToDestination(playerPosition);
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
        /// Searches for a player in a sphere area around it. Returns true if player is found and false otherwise.
        /// </summary>
        /// <returns>True if player is found and false otherwise.</returns>
        private bool LookForPlayer()
        {
            int found = Physics.OverlapSphereNonAlloc(actor.transform.position, lookRadius, playerInRange, playerMask);
            bool isFound = found == 1;
            if (isFound)
            {
                playerPosition = playerInRange[0].transform.position;
            }
            
            return isFound;
        }

        private void Attack()
        {
            actor.TriggerAnimation(attack);
        }
    }
}