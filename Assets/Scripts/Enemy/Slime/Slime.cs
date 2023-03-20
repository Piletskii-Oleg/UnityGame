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
    public class Slime : Enemy
    {
        private static readonly int mainTex = Shader.PropertyToID("_MainTex");

        private Material faceMaterial;
        
        private SlimeStateMachine stateMachine;

        public Vector3 LastHitPosition { get; private set; }
        
        /// <summary>
        /// Idle state of the slime.
        /// </summary>
        public IdleState IdleState { get; protected set; }

        /// <summary>
        /// Walk state of the slime.
        /// </summary>
        public WalkState WalkState { get; protected set; }

        /// <summary>
        /// Damaged state of the slime.
        /// </summary>
        public DamagedState DamagedState { get; protected set; }

        /// <summary>
        /// Attack state of the slime.
        /// </summary>
        public AttackState AttackState { get; protected set; }

        [Header("Slime Information")]
        [Tooltip("GameObject that contains the actor model")]
        [SerializeField]
        private GameObject slimeModel;
        [Tooltip("An object around which actor can roam freely")]
        [SerializeField]
        private SlimeArea slimeArea;
        [Tooltip("Scriptable object that contains all slime faces")]
        [SerializeField] private SlimeFacesList facesList;

        [Tooltip("Player character's transform (to follow them if attacked)")]
        [SerializeField] private Transform playerTransform;

        public NavMeshAgent Agent => agent;

        private void Start()
        {
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];
            
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            faceMaterial = slimeModel.GetComponent<Renderer>().materials[1];

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
        public void IdleForPeriod(float period, SlimeBaseState idleState, SlimeBaseState state)
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

        private IEnumerator IdleForPeriodCoroutine(float period, SlimeBaseState idleState, SlimeBaseState state)
        {
            stateMachine.ChangeState(idleState);
            yield return new WaitForSeconds(period);
            if (stateMachine.CurrentState == idleState)
            {
                stateMachine.ChangeState(state);
            }
        }
    }
}