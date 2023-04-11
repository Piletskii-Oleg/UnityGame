﻿using System.Collections.Generic;
using Enemy.Golem.ScriptableObjects;
using Enemy.Golem.States;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Golem
{
    public class Golem : Enemy
    {
        private static readonly int death = Animator.StringToHash("Die");

        [Header("Golem Stats")]
        [SerializeField] private Transform rightHand;
        [SerializeField] private float force;
        
        [Tooltip("Max angle at which golem can throw rocks (+angle and -angle from the forward vector)")]
        [SerializeField, Range(0f, 90f)] private float throwAngle;
        
        [SerializeField] private List<GameObject> stonePrefabs;
        [SerializeField] private List<GameObject> stonePrefabsRigidbody;

        private GameObject heldStone;

        /// <summary>
        /// Idle state of the golem.
        /// </summary>
        public IdleState IdleState { get; private set; } 

        /// <summary>
        /// Walk state of the golem.
        /// </summary>
        public WalkState WalkState { get; private set; }

        /// <summary>
        /// Damaged state of the golem.
        /// </summary>
        public DamagedState DamagedState { get; private set; }

        /// <summary>
        /// Attack state of the golem.
        /// </summary>
        public AttackState AttackState { get; private set; }
        
        /// <summary>
        /// Dead state of the golem.
        /// </summary>
        public DeadState DeadState { get; private set; }

        public NavMeshAgent Agent => agent;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];

            stateMachine = new GolemStateMachine();
            InitializeStates();

            stateMachine.Initialize(this.IdleState);
        }
        
        public override void OnKill()
        {
            base.OnKill();
            
            stateMachine.ChangeState(DeadState);
            
            TriggerAnimation(death);

            StartCoroutine(Disappear());
        }
        
        private void InitializeStates()
        {
            IdleState = new IdleState(this, stateMachine, followTimeTact);
            WalkState = new WalkState(this, stateMachine);
            DamagedState = new DamagedState(this, stateMachine);
            DeadState = new DeadState(this, stateMachine);
            AttackState = new AttackState(this, stateMachine);
        }

        public void SpawnStoneInHand()
        {
            heldStone = GameObjectSpawner.Spawn(stonePrefabs, rightHand);
        }

        public void ThrowStone()
        {
            var rotation = heldStone.transform.rotation;
            Destroy(heldStone);
            
            var position = rightHand.position;
            var stoneToThrow = GameObjectSpawner.Spawn(stonePrefabsRigidbody, position, rotation);

            var stoneScript = stoneToThrow.GetComponent<GolemStone>();
            stoneScript.Initialize(actorData.damage, actorData.affiliation, force);
            
            ThrowFromPosition(stoneToThrow, position);
        }

        private void ThrowFromPosition(GameObject stoneToThrow, Vector3 position)
        {
            var playerPosition = playerScriptableObject.GetActualPlayerPosition();
            var rigidBody = stoneToThrow.GetComponent<Rigidbody>();

            var forwardVector = position - transform.forward;
            var forceVector = (playerPosition - position).normalized * force;
            float angle = Vector3.Angle(forwardVector, forceVector);
            if (angle > throwAngle / 2)
            {
                forceVector = Quaternion.Euler(0, angle - lookAngle / 2, 0) * forceVector;
            }

            rigidBody.AddForce(forceVector, ForceMode.Impulse);
            rigidBody.AddTorque(GenerateRandomVector().normalized, ForceMode.Impulse);
        }

        private static Vector3 GenerateRandomVector(float minValue = 0f, float maxValue = 10f)
        {
            float valueX = Random.Range(minValue, maxValue);
            float valueY = Random.Range(minValue, maxValue);
            float valueZ = Random.Range(minValue, maxValue);
            return new Vector3(valueX, valueY, valueZ);
        }

        public void ChangeToIdle()
            => stateMachine.ChangeState(IdleState);

        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            base.OnTakeDamage(damage, actorAffiliation);
            
            if (stateMachine.CurrentState is not (States.AttackState or States.DamagedState or States.DeadState))
            {
                stateMachine.ChangeState(DamagedState);
            }
        }

        public Vector3 FindPlayer()
            => playerScriptableObject.GetActualPlayerPosition();
    }
}