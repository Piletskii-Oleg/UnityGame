using System.Collections.Generic;
using DG.Tweening;
using Enemy.Golem.States;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Golem
{
    public class Golem : Enemy
    {
        [Header("Base Position")]
        [SerializeField] private bool hasBasePoint;
        [SerializeField] private Transform basePoint;
        [SerializeField] private Quaternion baseRotation;

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
        public ChasePlayerState ChasePlayerState { get; private set; }

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
        
        /// <summary>
        /// Dead state of the golem.
        /// </summary>
        public ReturnToBaseState ReturnToBaseState { get; private set; }

        public NavMeshAgent Agent => agent;
        
        protected override void Awake()
        {
            base.Awake();
            
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();

            stateMachine = new GolemStateMachine();
            InitializeStates();

            stateMachine.Initialize(IdleState);
        }

        public override void OnKill()
        {
            if (!isKilled)
            {
                base.OnKill();

                stateMachine.ChangeState(DeadState);

                StartCoroutine(Disappear());
            }
        }

        private void InitializeStates()
        {
            IdleState = new IdleState(this, stateMachine, followTimeTact, hasBasePoint);
            ChasePlayerState = new ChasePlayerState(this, stateMachine);
            DamagedState = new DamagedState(this, stateMachine);
            DeadState = new DeadState(this, stateMachine);
            AttackState = new AttackState(this, stateMachine);

            if (hasBasePoint)
            {
                ReturnToBaseState = new ReturnToBaseState(this, stateMachine, basePoint);
            }
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

            var golemTransform = transform;
            var golemPosition = golemTransform.position;
            
            var forwardVector = golemTransform.rotation * Vector3.forward + golemPosition - golemPosition; // + x - x is used to move vector to golem
            var forceVector = (playerPosition - position).normalized * force;
            float angle = Vector3.Angle(forwardVector, forceVector);
            if (angle > throwAngle)
            {
                forceVector = Quaternion.Euler(0, angle - lookAngle / 2, 0) * forceVector;
            }

            rigidBody.AddForce(forceVector, ForceMode.Impulse);
            rigidBody.AddTorque(GenerateRandomVector().normalized, ForceMode.Impulse);
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

        public bool CheckBasePosition(Transform pointTransform)
            => Vector3.Distance(transform.position,pointTransform.position) < Mathf.Epsilon;

        public void TurnToBaseAngle()
        {
            transform.DORotateQuaternion(baseRotation, 0.7f);
        }

        public Vector3 FindPlayer()
            => playerScriptableObject.GetActualPlayerPosition();
    }
}