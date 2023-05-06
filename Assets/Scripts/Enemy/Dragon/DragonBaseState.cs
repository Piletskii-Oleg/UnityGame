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

        /// <summary>
        /// Used to stop DOTween sequences running on each state.
        /// </summary>
        public abstract void KillSequences();
    }
}