namespace Enemy.Dragon
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

        public abstract void KillSequences();
    }
}