using System.Collections.Generic;
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
            
            // caches player position to then make golem throw the stone at him
            PlayerPosition = playerScriptableObject.GetActualPlayerPosition();
        }

        public void ThrowStone()
        {
            var rotation = heldStone.transform.rotation;
            Destroy(heldStone);
            
            var position = rightHand.position;
            var stoneToThrow = GameObjectSpawner.Spawn(stonePrefabsRigidbody, position, rotation);

            var stoneScript = stoneToThrow.GetComponent<GolemStone>();
            stoneScript.Initialize(actorData.damage, actorData.affiliation, force);
            
            var rigidBody = stoneToThrow.GetComponent<Rigidbody>();
            var forceVector = (PlayerPosition - position).normalized * force;
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