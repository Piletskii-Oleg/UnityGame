namespace Enemy.Spider.States
{
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