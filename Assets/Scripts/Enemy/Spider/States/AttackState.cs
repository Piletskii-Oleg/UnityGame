using UnityEngine;

namespace Enemy.Spider.States
{
    public class AttackState : SpiderBaseState
    {
        private static readonly int attack = Animator.StringToHash("Attack");
        
        private float timePassed;
        private readonly float followTimeTact;

        private int timesPlayerIsNotFound;
        private readonly int timesPlayerIsSearched;

        public bool HasAttacked { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="AttackState"/> class.
        /// </summary>
        /// <param name="spider">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="followTimeTact">Time that should pass until spider looks for the player again.</param>
        /// <param name="timesPlayerIsSearched">Amount of times that spider will try to look for a player.</param>
        public AttackState(Spider spider, BaseStateMachine stateMachine, float followTimeTact, int timesPlayerIsSearched)
            : base(spider, stateMachine)
        {
            this.followTimeTact = followTimeTact;
            this.timesPlayerIsSearched = timesPlayerIsSearched;
        }

        public override void Enter()
        {
            timesPlayerIsNotFound = 0;
            UpdateTact();
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed >= followTimeTact)
            {
                UpdateTact();
            }

            if (spider.Agent.remainingDistance < spider.Agent.stoppingDistance)
            {
                Attack();
                spider.Stop();
                UpdateTact();
            }
        }

        /// <summary>
        /// Forces recalculation of player's position and the consequent behavior of the spider.
        /// </summary>
        private void UpdateTact()
        {
            timePassed = 0f;
            HasAttacked = false;
            
            if (spider.LookForPlayer())
            {
                spider.WalkToDestination(spider.PlayerPosition);
            }
            else
            {
                timesPlayerIsNotFound++;
            }

            if (timesPlayerIsNotFound > timesPlayerIsSearched)
            {
                spider.IdleForPeriod(0.3f, spider.IdleState, spider.WalkState);
            }
        }

        public override void Exit()
        {
        }

        /// <summary>
        /// Makes spider attack the player.
        /// </summary>
        private void Attack()
        {
            spider.TriggerAnimation(attack);
            HasAttacked = true;
        }
    }
}