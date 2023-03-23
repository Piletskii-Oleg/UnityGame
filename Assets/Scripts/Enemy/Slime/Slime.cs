﻿using Enemy.Slime.States;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemy.Slime
{
    /// <summary>
    /// A slime enemy actor.
    /// </summary>
    public class Slime : Enemy
    {
        private static readonly int mainTex = Shader.PropertyToID("_MainTex");

        private Material faceMaterial;

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

        [Header("Slime Information")]
        [Tooltip("GameObject that contains the actor model")]
        [SerializeField] private GameObject slimeModel;
        [FormerlySerializedAs("slimeArea")]
        [Tooltip("An object around which actor can roam freely")]
        [SerializeField] private CircleArea circleArea;
        [Tooltip("Scriptable object that contains all slime faces")]
        [SerializeField] private SlimeFacesList facesList;

        [Tooltip("Is the slime passive, neutral or aggressive towards player?")]
        [field: SerializeField] public SlimeType SlimeType { get; private set; }

        public NavMeshAgent Agent => agent;

        private void Start()
        {
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];
            
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            faceMaterial = slimeModel.GetComponent<Renderer>().materials[1];

            stateMachine = new SlimeStateMachine();
            IdleState = new IdleState(this, stateMachine, facesList.idleFace);
            WalkState = new WalkState(this, stateMachine, facesList.walkFace, circleArea);
            DamagedState = new DamagedState(this, stateMachine, facesList.damageFace, waitingTime);
            AttackState = new AttackState(this, stateMachine, facesList.attackFace, followTimeTact, timesPlayerIsSearched);

            stateMachine.Initialize(this.IdleState);
        }

        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            base.OnTakeDamage(damage, actorAffiliation);
            stateMachine.ChangeState(DamagedState);
        }

        /// <summary>
        /// Changes face of the slime to the given one.
        /// </summary>
        /// <param name="texture">Face to set.</param>
        public void SetFace(Texture texture)
            => faceMaterial.SetTexture(mainTex, texture);

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
        
        private void OnAnimatorMove()
        {
            var position = animator.rootPosition;
            position.y = agent.nextPosition.y;
            
            var enemyTransform = transform;
            
            enemyTransform.position = position;
            agent.nextPosition = enemyTransform.position;
        }
    }
}