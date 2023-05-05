using Enemy.Spider.States;
using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Spider
{
    public class Spider : Enemy
    {
        private static readonly int death = Animator.StringToHash("Die");

        [Header("Spider Stats")]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;

        /// <summary>
        /// Idle state of the spider.
        /// </summary>
        public IdleState IdleState { get; private set; } 

        /// <summary>
        /// Walk state of the spider.
        /// </summary>
        public WalkState WalkState { get; private set; }

        /// <summary>
        /// Damaged state of the spider.
        /// </summary>
        public DamagedState DamagedState { get; private set; }

        /// <summary>
        /// Attack state of the spider.
        /// </summary>
        public AttackState AttackState { get; private set; }
        
        /// <summary>
        /// Dead state of the spider.
        /// </summary>
        public DeadState DeadState { get; private set; }
        
        /// <summary>
        /// State of the spider when it just spawned.
        /// </summary>
        public SpawnedState SpawnedState { get; private set; }
        
        /// <summary>
        /// The <see cref="NavMeshAgent"/> component of the spider.
        /// </summary>
        public NavMeshAgent Agent => agent;
        
        protected override void Awake()
        {
            base.Awake();
            
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];

            stateMachine = new SpiderStateMachine();
            InitializeStates();

            stateMachine.Initialize(IdleState);
        }

        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            base.OnTakeDamage(damage, actorAffiliation);

            if (stateMachine.CurrentState is not (States.AttackState or States.DamagedState or States.DeadState))
            {
                stateMachine.ChangeState(DamagedState);
            }
        }

        /// <summary>
        /// Deals damage to the given actor,
        /// but only if the spider is in the <see cref="AttackState"/> and has not attacked yet
        /// (in the current tact of <see cref="AttackState"/>).
        /// </summary>
        /// <param name="actor">Actor that the spider should deal damage to.</param>
        public void DealDamage(Actor actor)
        {
            if (stateMachine.CurrentState is AttackState && !AttackState.HasAttacked)
            {
                actor.OnTakeDamage(actorData.damage, actorData.affiliation);
            }
        }

        /// <summary>
        /// Gets a new position in the <see cref="area"/>.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetNewPositionInArea()
            => area.GetNewPosition();

        /// <summary>
        /// Gets a new position around the spider with the radius of lookRadius.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetNewPositionInLocalArea()
            => CircleAreaBase.GetNewPosition(lookRadius, transform.position);

        /// <summary>
        /// Gets position of the player in the world.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetActualPlayerPosition()
            => playerScriptableObject.GetActualPlayerPosition();

        public override void OnKill()
        {
            if (!isKilled)
            {
                base.OnKill();

                stateMachine.ChangeState(DeadState);

                TriggerAnimation(death);

                StartCoroutine(Disappear());
            }
        }

        private void InitializeStates()
        {
            IdleState = new IdleState(this, stateMachine);
            WalkState = new WalkState(this, stateMachine);
            DamagedState = new DamagedState(this, stateMachine);
            DeadState = new DeadState(this, stateMachine);
            AttackState = new AttackState(this, stateMachine, followTimeTact, timesPlayerIsSearched, walkSpeed,
                runSpeed);
        }
    }
}