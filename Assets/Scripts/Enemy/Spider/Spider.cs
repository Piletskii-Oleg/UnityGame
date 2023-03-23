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
        
        public NavMeshAgent Agent => agent;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            
            playerMask = 1 << LayerMask.NameToLayer("Player");
            playerInRange = new Collider[1];

            stateMachine = new SpiderStateMachine();
            IdleState = new IdleState(this, stateMachine);
            WalkState = new WalkState(this, stateMachine, area);
            DamagedState = new DamagedState(this, stateMachine);
            DeadState = new DeadState(this, stateMachine);
            AttackState = new AttackState(this, stateMachine, followTimeTact, timesPlayerIsSearched);

            stateMachine.Initialize(this.IdleState);
        }
        
        public override void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            base.OnTakeDamage(damage, actorAffiliation);
            stateMachine.ChangeState(DamagedState);
        }

        public void DealDamage(Actor actor)
        {
            if (stateMachine.CurrentState is AttackState && !AttackState.HasAttacked)
            {
                actor.OnTakeDamage(damage, affiliation);
            }
        }

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
    }
}