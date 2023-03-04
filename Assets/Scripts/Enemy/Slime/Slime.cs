using System;
using System.Collections;
using Enemy.Slime.States;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Slime
{
    public class Slime : Actor
    {
        private NavMeshAgent agent;
        private Animator animator;
        private SlimeStateMachine stateMachine;
        private SlimeFacesList facesList;

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

            stateMachine = new SlimeStateMachine(facesList);
            
            IdleState = new IdleState(this, stateMachine);
            WalkState = new WalkState(this, stateMachine, waypoints);
            DamagedState = new DamagedState(this, stateMachine);
            AttackState = new AttackState(this, stateMachine);
            
            stateMachine.Initialize(this.IdleState);
        }

        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            base.OnTakeDamage(damage, actorAffiliation);
            stateMachine.ChangeState(DamagedState);
        }

        private void Update()
        {
            stateMachine.CurrentState.Tick();
        }

        public void SetAnimationValue(int animationHash, float value)
            => animator.SetFloat(animationHash, value);

        public void SetAnimationValue(int animationHash, bool value)
            => animator.SetBool(animationHash, value);

        public void SetAnimationValue(int animationHash, int value)
            => animator.SetInteger(animationHash, value);

        public void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);

        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            transform.position = position;
            agent.nextPosition = transform.position;
        }
    }
}