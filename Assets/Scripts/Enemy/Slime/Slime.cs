using System.Collections;
using Enemy.Slime.States;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Slime
{
    /// <summary>
    /// A slime enemy actor.
    /// </summary>
    public class Slime : Actor
    {
        private static readonly int doStep = Animator.StringToHash("DoStep");
        private static readonly int mainTex = Shader.PropertyToID("_MainTex");
        
        private LayerMask playerMask;
        private Collider[] playerInRange;

        private NavMeshAgent agent;
        private Animator animator;
        private SlimeStateMachine stateMachine;
        private Material faceMaterial;
        
        private Transform slimeTransform;

        public Vector3 LastHitPosition { get; private set; }

        [Header("General information")]
        [Tooltip("GameObject that contains the slime model")]
        [SerializeField] private GameObject slimeModel;
        [Tooltip("Scriptable object that contains all slime faces")]
        [SerializeField] private SlimeFacesList facesList;
        [Tooltip("An object around which slime can roam freely")]
        [SerializeField] private SlimeArea slimeArea;
        [Tooltip("Is the slime passive, neutral or aggressive towards player?")]
        [field: SerializeField] public SlimeType SlimeType { get; private set; }
        
        /// <summary>
        /// Position of the player calculated using <see cref="LookForPlayer"/> method.
        /// </summary>
        public Vector3 PlayerPosition { get; private set; }

        [Header("Attack State")]
        [Tooltip("Radius of a circle in which slime will look for the player")]
        [SerializeField] private float lookRadius;
        [Tooltip("Angle of a segment of a circle in which slime can see the player")]
        [SerializeField] private float lookAngle;
        [Tooltip("Time that should pass until slime looks for the player again")]
        [SerializeField] private float followTimeTact;
        [Tooltip("Amount of times that slime will try to look for a player")]
        [SerializeField] private int timesPlayerIsSearched;
        [SerializeField] private float damage;
        
        [Header("Damaged State")]
        [Tooltip("Amount of time which slime stays in damaged state for")]
        [SerializeField] [Range(0.0f, 3.0f)] private float waitingTime;
        [Tooltip("Player character's transform (to follow them if attacked)")]
        [SerializeField] private Transform playerTransform;

        /// <summary>
        /// Idle state of the slime.
        /// </summary>
        public IdleState IdleState { get; private set; }

        /// <summary>
        /// Walk state of the slime.
        /// </summary>
        public WalkState WalkState { get; private set; }
        
        /// <summary>
        /// Damaged state of the slime.
        /// </summary>
        public DamagedState DamagedState { get; private set; }
        
        /// <summary>
        /// Attack state of the slime.
        /// </summary>
        public AttackState AttackState { get; private set; }

        public NavMeshAgent Agent => agent;

        private void Start()
        {
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];
            
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            faceMaterial = slimeModel.GetComponent<Renderer>().materials[1];

            slimeTransform = GetComponent<Transform>();

            stateMachine = new SlimeStateMachine(facesList);
            IdleState = new IdleState(this, stateMachine, facesList.idleFace);
            WalkState = new WalkState(this, stateMachine, facesList.walkFace, slimeArea);
            DamagedState = new DamagedState(this, stateMachine, facesList.damageFace, waitingTime, playerTransform);
            AttackState = new AttackState(this, stateMachine, facesList.attackFace, followTimeTact, timesPlayerIsSearched);

            stateMachine.Initialize(this.IdleState);
        }

        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation, Vector3 hitPosition)
        {
            base.OnTakeDamage(damage, actorAffiliation, hitPosition);
            LastHitPosition = hitPosition;
            stateMachine.ChangeState(DamagedState);
        }

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
        /// Changes face of the slime to the given one.
        /// </summary>
        /// <param name="texture">Face to set.</param>
        public void SetFace(Texture texture)
            => faceMaterial.SetTexture(mainTex, texture);
        
        /// <summary>
        /// Changes slime's state to the <paramref name="idleState"/> for a <paramref name="period"/> seconds
        /// and then changes it to <paramref name="state"/>.
        /// </summary>
        /// <param name="period">Time in seconds for which slime should idle.</param>
        /// <param name="idleState">Slime's idle state.</param>
        /// <param name="state">New state, activated after <paramref name="period"/> seconds.</param>
        public void IdleForPeriod(float period, BaseState idleState, BaseState state)
            => StartCoroutine(IdleForPeriodCoroutine(period, idleState, state));

        private void OnCollisionEnter(Collision collision)
        {
            if (stateMachine.CurrentState != AttackState)
            {
                return;
            }
            
            if (collision.gameObject.TryGetComponent<Actor>(out var actor))
            {
                actor.OnTakeDamage(damage, affiliation);
            }
        }

        private IEnumerator IdleForPeriodCoroutine(float period, BaseState idleState, BaseState state)
        {
            stateMachine.ChangeState(idleState);
            yield return new WaitForSeconds(period);
            if (stateMachine.CurrentState == idleState)
            {
                stateMachine.ChangeState(state);
            }
        }

        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            slimeTransform.position = position;
            agent.nextPosition = slimeTransform.position;
        }

        /// <summary>
        /// Activates the walk animation and makes slime move towards its next destination.
        /// </summary>
        public void WalkToDestination(Vector3 destination)
        {
            SetAnimationValue(doStep, true);
            Agent.SetDestination(destination);
            Agent.isStopped = false;
        }

        /// <summary>
        /// Stops the movement of the slime.
        /// </summary>
        public void Stop()
        {
            SetAnimationValue(doStep, false);
            Agent.isStopped = true;
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

            var position = slimeTransform.position;
            var directionToTarget = (position - PlayerPosition).normalized;
            return (Vector3.Angle(position, directionToTarget) < lookAngle / 2);
        }
    }
}