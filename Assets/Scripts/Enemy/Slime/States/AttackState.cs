using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// State that corresponds to the slime attacking the player.
    /// </summary>
    public class AttackState : BaseState
    {
        private static readonly int attack = Animator.StringToHash("Attack");

        private float lookRadius = 10f;
        private readonly LayerMask playerMask = 1 << LayerMask.NameToLayer("Player");
        private readonly Collider[] playerInRange = new Collider[1];
        private Vector3 playerPosition;

        private float timePassed;
        private float followTimeTact = 1f;

        private int timesPlayerIsNotFound;
        private readonly int maxTimesPlayerIsNotFound = 3;
        
        /// <summary>
        /// Initializes new instance of <see cref="AttackState"/> class.
        /// </summary>
        /// <param name="actor">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="stateFace">Slime face that corresponds to this state.</param>
        public AttackState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
            : base(actor, stateMachine, stateFace)
        {
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

            if (timesPlayerIsNotFound > maxTimesPlayerIsNotFound)
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
            if (found == 1)
            {
                playerPosition = playerInRange[0].transform.position;
            }
            
            return found == 1;
        }

        private void Attack()
        {
            actor.TriggerAnimation(attack);
        }
    }
}