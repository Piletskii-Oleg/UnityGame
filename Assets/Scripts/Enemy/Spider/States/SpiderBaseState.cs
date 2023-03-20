namespace Enemy.Spider.States
{
    /// <summary>
    /// Base state of a spider from which all other inherit.
    /// </summary>
    public abstract class SpiderBaseState : BaseState
    {
        protected readonly Spider spider;
        protected readonly BaseStateMachine stateMachine;

        protected SpiderBaseState(Spider spider, BaseStateMachine stateMachine)
        {
            this.spider = spider;
            this.stateMachine = stateMachine;
        }
    }
}