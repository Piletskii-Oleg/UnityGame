using Core.Enemy;
using UnityEngine;

namespace Functionality.Enemy.Spider.States
{
    /// <summary>
    /// Slime that corresponds to the spider being idle.
    /// </summary>
    public class IdleState : SpiderBaseState
    {
        private float timePassed; 
        private float timeLimit;
        
        public IdleState(Spider spider, BaseStateMachine stateMachine)
            : base(spider, stateMachine)
        {
        }
        
        public override void Enter()
        {
            spider.Stop();

            timeLimit = Random.Range(0.6f, 1.3f);
        }

        public override void Tick()
        {
            timePassed += Time.deltaTime;
            if (timePassed > timeLimit)
            {
                timePassed = 0;
                stateMachine.ChangeState(spider.WalkState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}