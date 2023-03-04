using System;
using System.Collections;
using System.Linq;
using Enemy.Slime.States;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Slime
{
    public class Slime : Actor
    {
        private static readonly int mainTex = Shader.PropertyToID("_MainTex");
        
        private NavMeshAgent agent;
        private Animator animator;
        private AnimationClip[] animationClips;
        private SlimeStateMachine stateMachine;
        private Material faceMaterial;

        [SerializeField] private GameObject slimeModel;
        [SerializeField] private SlimeFacesList facesList;
        [SerializeField] private Transform[] waypoints;
        

        public IdleState IdleState { get; private set; }

        public WalkState WalkState { get; private set; }
        
        public DamagedState DamagedState { get; private set; }
        
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
            WalkState = new WalkState(this, stateMachine, facesList.walkFace, waypoints);
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

        public void SetAnimationValue(int animationHash, float value)
            => animator.SetFloat(animationHash, value);

        public void SetAnimationValue(int animationHash, bool value)
            => animator.SetBool(animationHash, value);

        public void SetAnimationValue(int animationHash, int value)
            => animator.SetInteger(animationHash, value);

        public void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);
        
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