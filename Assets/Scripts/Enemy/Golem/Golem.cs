using System.Collections;
using System.Collections.Generic;
using Enemy.Golem.States;
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
            heldStone = StoneSpawner.Spawn(stonePrefabs, rightHand);
            PlayerPosition = playerScriptableObject.GetActualPlayerPosition();
        }

        public void ThrowStone()
        {
            var rotation = heldStone.transform.rotation;
            Destroy(heldStone);
            
            var position = rightHand.position;
            var stoneToThrow = StoneSpawner.Spawn(stonePrefabsRigidbody, position, rotation);
            var rigidBody = stoneToThrow.GetComponent<Rigidbody>();

            var forceVector = (PlayerPosition - position).normalized * force + Vector3.up * (force * 0.28f);
            rigidBody.AddForce(forceVector, ForceMode.Impulse);
            rigidBody.AddTorque(Vector3.up, ForceMode.Impulse);
        }

        public void ChangeToIdle()
        {
            stateMachine.ChangeState(IdleState);
        }

        public Vector3 FindPlayer()
            => playerScriptableObject.GetActualPlayerPosition();
    }
}