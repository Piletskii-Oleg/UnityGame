namespace Enemy.Dragon.States
{
    public abstract class DragonBaseState : BaseState
    {
        private readonly Dragon dragon;
        private readonly BaseStateMachine stateMachine;

        protected DragonBaseState(BaseStateMachine stateMachine, Dragon dragon)
        {
            this.stateMachine = stateMachine;
            this.dragon = dragon;
        }
    }
}