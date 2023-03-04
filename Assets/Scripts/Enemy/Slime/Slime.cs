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
        private static readonly int mainTex = Shader.PropertyToID("_MainTex");
        
        private NavMeshAgent agent;
        private Animator animator;
        private AnimationClip[] animationClips;
        private SlimeStateMachine stateMachine;
        private Material faceMaterial;

        [Tooltip("GameObject that contains the slime model")]
        [SerializeField] private GameObject slimeModel;
        [Tooltip("Scriptable object that contains all slime faces")]
        [SerializeField] private SlimeFacesList facesList;
        [Tooltip("An object around which slime can roam freely")]
        [SerializeField] private SlimeArea slimeArea;

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
        
        public Animator Animator => animator;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            animationClips = animator.runtimeAnimatorController.animationClips;
            faceMaterial = slimeModel.GetComponent<Renderer>().materials[1];

            stateMachine = new SlimeStateMachine(facesList);
            
            IdleState = new IdleState(this, stateMachine, facesList.idleFace);
            WalkState = new WalkState(this, stateMachine, facesList.walkFace, slimeArea);
            DamagedState = new DamagedState(this, stateMachine, facesList.damageFace);
            AttackState = new AttackState(this, stateMachine, facesList.attackFace);

            stateMachine.Initialize(this.IdleState);
        }

        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            base.OnTakeDamage(damage, actorAffiliation);
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

        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            transform.position = position;
            agent.nextPosition = transform.position;
        }
    }
}