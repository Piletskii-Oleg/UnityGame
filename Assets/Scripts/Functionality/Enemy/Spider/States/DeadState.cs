using Core.Enemy;

namespace Functionality.Enemy.Spider.States
{
    /// <summary>
    /// Slime that corresponds to the spider being dead.
    /// </summary>
    public class DeadState : SpiderBaseState
    {
        public DeadState(Spider spider, BaseStateMachine stateMachine)
            : base(spider, stateMachine)
        {
        }

        public override void Enter()
        {
            spider.Stop();
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}