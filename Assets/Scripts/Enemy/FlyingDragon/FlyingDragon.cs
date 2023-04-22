﻿using Enemy.FlyingDragon.States;
using UnityEngine;

namespace Enemy.FlyingDragon
{
    public class FlyingDragon : MonoBehaviour
    {
        private Animator animator;
        private FlyingStateMachine stateMachine;

        [SerializeField] private Transform[] points;
        
        public FlyToPointState FlyToPointState { get; set; }
        
        public StandState StandState { get; set; }
        
        public FlyAroundState FlyAroundState { get; set; }
        
        public TakeOffState TakeOffState { get; set; }
        
        public LandingState LandingState { get; set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            stateMachine = new FlyingStateMachine();
            var clips = animator.runtimeAnimatorController.animationClips;
            
            FlyToPointState = new FlyToPointState(this, stateMachine, points);
            StandState = new StandState(this, stateMachine);
            FlyAroundState = new FlyAroundState(this, stateMachine);
            TakeOffState = new TakeOffState(this, stateMachine);
            LandingState = new LandingState(this, stateMachine);
            
            stateMachine.Initialize(StandState);
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
    }
}