namespace Enemy.Dragon.States
{
    public abstract class DragonBaseState : BaseState
    {
        protected readonly Dragon dragon;
        protected readonly BaseStateMachine stateMachine;

        protected DragonBaseState(BaseStateMachine stateMachine, Dragon dragon)
        {
            this.stateMachine = stateMachine;
            this.dragon = dragon;
        }
    }
}