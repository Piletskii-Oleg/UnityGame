using Enemy.Spider.States;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Spider
{
    public class Spider : Enemy
    {
        /// <summary>
        /// Idle state of the slime.
        /// </summary>
        public IdleState IdleState { get; protected set; }

        /// <summary>
        /// Walk state of the slime.
        /// </summary>
        public WalkState WalkState { get; protected set; }

        /// <summary>
        /// Damaged state of the slime.
        /// </summary>
        public DamagedState DamagedState { get; protected set; }

        /// <summary>
        /// Attack state of the slime.
        /// </summary>
        public AttackState AttackState { get; protected set; }
        
        public NavMeshAgent Agent => agent;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];

            stateMachine = new SpiderStateMachine();
            IdleState = new IdleState(this, stateMachine);
            WalkState = new WalkState(this, stateMachine);
            DamagedState = new DamagedState(this, stateMachine);
            AttackState = new AttackState(this, stateMachine);

            stateMachine.Initialize(this.IdleState);
        }
    }
}