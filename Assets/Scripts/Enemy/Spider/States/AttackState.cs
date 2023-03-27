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

        private readonly float walkSpeed;
        private readonly float runSpeed;

        private bool isPlayerReachable;
        private readonly float playerReachThreshold = 5f;

        public bool HasAttacked { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="AttackState"/> class.
        /// </summary>
        /// <param name="spider">Actor that references this state.</param>
        /// <param name="stateMachine">State machine that will use with this state.</param>
        /// <param name="followTimeTact">Time that should pass until spider looks for the player again.</param>
        /// <param name="timesPlayerIsSearched">Amount of times that spider will try to look for a player.</param>
        /// <param name="walkSpeed">Speed with which the spider walks around.</param>
        /// <param name="runSpeed">Speed with which the spider follows the player.</param>
        public AttackState(Spider spider, BaseStateMachine stateMachine, float followTimeTact,
            int timesPlayerIsSearched, float walkSpeed, float runSpeed)
            : base(spider, stateMachine)
        {
            this.followTimeTact = followTimeTact;
            
            this.timesPlayerIsSearched = timesPlayerIsSearched;
            
            this.walkSpeed = walkSpeed;
            this.runSpeed = runSpeed;
        }

        public override void Enter()
            => UpdateTact();

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed >= followTimeTact)
            {
                UpdateTact();
                return;
            }

            if (spider.Agent.remainingDistance < spider.Agent.stoppingDistance && isPlayerReachable)
            {
                Attack();
            }
            
            UpdateTact();
        }

        /// <summary>
        /// Forces recalculation of player's position and the consequent behavior of the spider.
        /// </summary>
        private void UpdateTact()
        {
            ResetValues();

            if (spider.LookForPlayer())
            {
                spider.WalkToDestination(spider.PlayerPosition);
                
            }
            else
            {
                spider.WalkToDestination(spider.GetNewPositionInLocalArea());
                timesPlayerIsNotFound++;
            }

            if (timesPlayerIsNotFound > timesPlayerIsSearched)
            {
                spider.IdleForPeriod(0.3f, spider.IdleState, spider.WalkState);
            }
        }

        private void ResetValues()
        {
            timePassed = 0f;
            HasAttacked = false;
            spider.Agent.speed = runSpeed;
            timesPlayerIsNotFound = 0;
            isPlayerReachable = Mathf.Abs(spider.PlayerPosition.y - spider.transform.position.y) < playerReachThreshold;
        }

        public override void Exit()
        {
            spider.Agent.speed = walkSpeed;
        }

        /// <summary>
        /// Makes spider attack the player.
        /// </summary>
        private void Attack()
        {
            spider.Stop();

            spider.TriggerAnimation(attack);
            
            HasAttacked = true;
        }
    }
}