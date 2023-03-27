using System.Collections;
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
        
        [SerializeField] private CircleArea area;
        
        [Tooltip("Time after which the spider disappears (applies after death)")]
        [SerializeField] private float timeUntilDisappearing;

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
        
        public DeadState DeadState { get; private set; }
        
        public SpawnedState SpawnedState { get; private set; }
        
        public NavMeshAgent Agent => agent;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];

            stateMachine = new SpiderStateMachine();
            InitializeStates();

            stateMachine.Initialize(this.IdleState);
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
                actor.OnTakeDamage(damage, affiliation);
            }
        }
        
        /// <summary>
        /// Spawns the spider and makes it move to the specified position.
        /// </summary>
        /// <param name="position">Position for the spider to move to.</param>
        public void GetSpawned(Vector3 position)
        {
            SpawnedState = new SpawnedState(this, stateMachine, position);
            stateMachine.ChangeState(SpawnedState);
        }

        /// <summary>
        /// Sets the <see cref="area"/> variable
        /// (if spider is spawned, it cannot be initialized in the editor as it is scene-specific).
        /// </summary>
        /// <param name="newArea">Area which the spider belongs to.</param>
        public void SetArea(CircleArea newArea)
            => area = newArea;

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
            => CircleArea.GetNewPosition(lookRadius, transform.position);

        /// <summary>
        /// Gets position of the player in the world.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetActualPlayerPosition()
            => playerScriptableObject.GetActualPlayerPosition();

        public override void OnKill()
        {
            base.OnKill();
            
            stateMachine.ChangeState(DeadState);
            
            TriggerAnimation(death);

            StartCoroutine(Disappear());
        }

        private IEnumerator Disappear()
        {
            yield return new WaitForSeconds(timeUntilDisappearing);
            
            gameObject.SetActive(false);
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