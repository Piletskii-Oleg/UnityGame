namespace Enemy.FlyingDragon.States
{
    public abstract class FlyingBaseState : BaseState
    {
        protected readonly FlyingDragon dragon;
        protected readonly BaseStateMachine stateMachine;
        
        protected FlyingBaseState(FlyingDragon dragon, BaseStateMachine stateMachine)
        {
            this.dragon = dragon;
            this.stateMachine = stateMachine;
        }

        public virtual void KillSequences()
        {
        }
    }
}