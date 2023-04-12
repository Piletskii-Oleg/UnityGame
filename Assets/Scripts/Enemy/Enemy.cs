using System.Collections;
using Player.ScriptableObjects;
using Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Enemy : Actor
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        
        protected BaseStateMachine stateMachine;
        
        protected LayerMask playerMask;
        protected Collider[] playerInRange;
        
        protected NavMeshAgent agent;
        protected Animator animator;

        [Header("Player")]
        [SerializeField] protected PlayerScriptableObject playerScriptableObject;
        
        [Header("Attack State")]
        [Tooltip("Radius of a circle in which enemy will look for the player")]
        [SerializeField] protected float lookRadius;

        [Tooltip("Angle of a segment of a circle in which enemy can see the player")]
        [SerializeField] protected float lookAngle;

        [Tooltip("Time in seconds that should pass until enemy looks for the player again")]
        [SerializeField] protected float followTimeTact;

        [Tooltip("Amount of times that enemy will try to look for a player")]
        [SerializeField] protected int timesPlayerIsSearched;

        [Header("Damaged State")]
        [Tooltip("Amount of time which actor stays in damaged state for")]
        [SerializeField] [Range(0.0f, 3.0f)]
        protected float waitingTime;

        [Tooltip("Time in seconds after which the enemy disappears (applies after death)")]
        [SerializeField] private float timeUntilDisappearing;

        /// <summary>
        /// Position of the player calculated using <see cref="LookForPlayer"/> method.
        /// </summary>
        public Vector3 PlayerPosition { get; protected set; }

        private void Update()
            => stateMachine.CurrentState.Tick();
        
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
            SetAnimationValue(doStep, false);
            agent.isStopped = true;
        }

        /// <summary>
        /// Searches for a player in a sphere area around it. Returns true if player is found and false otherwise.
        /// </summary>
        /// <returns>True if player is found and false otherwise.</returns>
        public bool LookForPlayer()
        {
            PlayerPosition = playerScriptableObject.GetActualPlayerPosition();
            
            int found = Physics.OverlapSphereNonAlloc(transform.position, lookRadius, playerInRange, playerMask);
            
            return found == 1;
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

            var enemyTransform = transform;
            var position = enemyTransform.position;
            var directionToTarget = (PlayerPosition - position).normalized;

            float angle = Vector3.Angle(enemyTransform.forward, directionToTarget);
            return angle < lookAngle / 2;
        }
        
        /// <summary>
        /// Changes enemy's state to the <paramref name="idleState"/> for a <paramref name="period"/> seconds
        /// and then changes it to <paramref name="state"/>.
        /// </summary>
        /// <param name="period">Time in seconds for which enemy should idle.</param>
        /// <param name="idleState">Enemy's idle state.</param>
        /// <param name="state">New state, activated after <paramref name="period"/> seconds.</param>
        public void IdleForPeriod(float period, BaseState idleState, BaseState state)
            => StartCoroutine(IdleForPeriodCoroutine(period, idleState, state));
        
        private IEnumerator IdleForPeriodCoroutine(float period, BaseState idleState, BaseState state)
        {
            stateMachine.ChangeState(idleState);
            
            yield return new WaitForSeconds(period);
            
            if (stateMachine.CurrentState == idleState)
            {
                stateMachine.ChangeState(state);
            }
        }

        protected IEnumerator Disappear()
        {
            yield return new WaitForSeconds(timeUntilDisappearing);
            
            gameObject.SetActive(false);
        }
    }
}