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
        
        private NavMeshAgent agent;
        private Animator animator;
        private SlimeStateMachine stateMachine;
        private Material faceMaterial;
        
        private Transform slimeTransform;

        public Vector3 LastHitPosition { get; private set; }

        [Tooltip("GameObject that contains the slime model")]
        [SerializeField] private GameObject slimeModel;
        [Tooltip("Scriptable object that contains all slime faces")]
        [SerializeField] private SlimeFacesList facesList;
        [Tooltip("An object around which slime can roam freely")]
        [SerializeField] private SlimeArea slimeArea;

        [Tooltip("Is the slime passive, neutral or aggressive towards player?")]
        [field: SerializeField] public SlimeType SlimeType { get; private set; }

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
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            faceMaterial = slimeModel.GetComponent<Renderer>().materials[1];

            slimeTransform = GetComponent<Transform>();

            stateMachine = new SlimeStateMachine(facesList);

            IdleState = new IdleState(this, stateMachine, facesList.idleFace);
            WalkState = new WalkState(this, stateMachine, facesList.walkFace, slimeArea);
            DamagedState = new DamagedState(this, stateMachine, facesList.damageFace);
            AttackState = new AttackState(this, stateMachine, facesList.attackFace);

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
        
        public void IdleForPeriod(float period, BaseState idleState, BaseState state)
            => StartCoroutine(IdleForPeriodCoroutine(period, idleState, state));

        private IEnumerator IdleForPeriodCoroutine(float period, BaseState idleState, BaseState state)
        {
            stateMachine.ChangeState(idleState);
            yield return new WaitForSeconds(period);
            stateMachine.ChangeState(state);
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
    }
}