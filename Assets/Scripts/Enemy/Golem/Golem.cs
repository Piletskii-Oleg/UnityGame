using Enemy.Golem.States;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Golem
{
    public class Golem : Enemy
    {
        private static readonly int death = Animator.StringToHash("Die");
        
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
            IdleState = new IdleState(this, stateMachine);
            WalkState = new WalkState(this, stateMachine);
            DamagedState = new DamagedState(this, stateMachine);
            DeadState = new DeadState(this, stateMachine);
            AttackState = new AttackState(this, stateMachine);
        }
    }
}