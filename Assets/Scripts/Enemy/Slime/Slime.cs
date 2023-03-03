using System;
using System.Collections;
using Enemy.Slime.States;
using Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Slime
{
    public class Slime : Actor
    {
        private NavMeshAgent agent;
        private Animator animator;

        private SlimeStateMachine stateMachine;

        [SerializeField] private Transform[] waypoints;

        public IdleState IdleState { get; private set; }
        
        public JumpState JumpState { get; private set; }

        public WalkState WalkState { get; private set; }

        public NavMeshAgent Agent => agent;
        public Animator Animator => animator;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            stateMachine = new SlimeStateMachine();
            
            IdleState = new IdleState(this, stateMachine);
            JumpState = new JumpState(this, stateMachine);
            WalkState = new WalkState(this, stateMachine, waypoints);
            
            stateMachine.Initialize(this.IdleState);
        }

        private void Update()
        {
            stateMachine.CurrentState.Tick();
        }

        public void SetAnimationValue(int animationHash, float value)
            => animator.SetFloat(animationHash, value);

        public void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);

        public void StopAnimation(int animationHash)
        {
            var info = animator.GetCurrentAnimatorStateInfo(0);
            SetAnimationValue(animationHash, 0);
            agent.isStopped = true;
        }

        private IEnumerator WaitTillAnimationIsComplete(AnimatorStateInfo info, int animationHash)
        {
            yield return null;


        }
        
        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            transform.position = position;
            agent.nextPosition = transform.position;
        }
    }
}