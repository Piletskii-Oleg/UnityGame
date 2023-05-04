namespace Enemy.Golem.States
{
    public abstract class GolemBaseState : BaseState
    {
        protected readonly Golem golem;
        protected readonly BaseStateMachine stateMachine;
        
        protected GolemBaseState(Golem golem, BaseStateMachine stateMachine)
        {
            this.golem = golem;
            this.stateMachine = stateMachine;
        }
    }
}