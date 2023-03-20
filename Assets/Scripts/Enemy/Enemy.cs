using Enemy.Slime;
using Enemy.Slime.States;
using Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : Actor
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        
        protected LayerMask playerMask;
        protected Collider[] playerInRange;
        
        protected NavMeshAgent agent;
        protected Animator animator;

        [Header("Attack State")]
        [Tooltip("Radius of a circle in which actor will look for the player")]
        [SerializeField] private float lookRadius;

        [Tooltip("Angle of a segment of a circle in which actor can see the player")]
        [SerializeField] private float lookAngle;

        [Tooltip("Time that should pass until actor looks for the player again")]
        [SerializeField]
        protected float followTimeTact;

        [Tooltip("Amount of times that actor will try to look for a player")]
        [SerializeField]
        protected int timesPlayerIsSearched;

        [SerializeField] protected float damage;

        [Header("Damaged State")]
        [Tooltip("Amount of time which actor stays in damaged state for")]
        [SerializeField] [Range(0.0f, 3.0f)]
        protected float waitingTime;

        [Tooltip("Is the actor passive, neutral or aggressive towards player?")]
        [field: SerializeField] public SlimeType SlimeType { get; private set; }

        /// <summary>
        /// Position of the player calculated using <see cref="LookForPlayer"/> method.
        /// </summary>
        public Vector3 PlayerPosition { get; protected set; }

        /// <summary>
        /// Sets a value of an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        /// <param name="value">Value to set.</param>
        public void SetAnimationValue(int animationHash, float value)
            => animator.SetFloat(animationHash, value);

        /// <summary>
        /// Sets a value of an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        /// <param name="value">Value to set.</param>
        public void SetAnimationValue(int animationHash, bool value)
            => animator.SetBool(animationHash, value);

        /// <summary>
        /// Sets a value of an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        /// <param name="value">Value to set.</param>
        public void SetAnimationValue(int animationHash, int value)
            => animator.SetInteger(animationHash, value);

        /// <summary>
        /// Triggers an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        public void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);

        protected void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            transform.position = position;
            agent.nextPosition = transform.position;
        }

        /// <summary>
        /// Activates the walk animation and makes actor move towards its next destination.
        /// </summary>
        public void WalkToDestination(Vector3 destination)
        {
            SetAnimationValue(doStep, true);
            agent.SetDestination(destination);
            agent.isStopped = false;
        }

        /// <summary>
        /// Stops the movement of the actor.
        /// </summary>
        public void Stop()
        {
            SetAnimationValue(Slime.Slime.doStep, false);
            agent.isStopped = true;
        }

        /// <summary>
        /// Searches for a player in a sphere area around it. Returns true if player is found and false otherwise.
        /// </summary>
        /// <returns>True if player is found and false otherwise.</returns>
        public bool LookForPlayer()
        {
            int found = Physics.OverlapSphereNonAlloc(transform.position, lookRadius, playerInRange, playerMask);
            bool isFound = found == 1;
            if (isFound)
            {
                PlayerPosition = playerInRange[0].transform.position;
            }
            
            return isFound;
        }

        /// <summary>
        /// Searches for a player in a sphere around it and limits its view to a segment with angle <see cref="lookAngle"/>.
        /// Returns true if player is found and false otherwise.
        /// </summary>
        /// <returns>True if player is found and false otherwise.</returns>
        public bool LookForPlayerInSegment()
        {
            if (!LookForPlayer())
            {
                return false;
            }

            var position = transform.position;
            var directionToTarget = (position - PlayerPosition).normalized;
            return (Vector3.Angle(position, directionToTarget) < lookAngle / 2);
        }
    }
}