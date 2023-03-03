namespace Enemy.Slime.States
{
    public abstract class BaseState
    {
        protected Slime actor;
        protected SlimeStateMachine stateMachine;

        protected BaseState(Slime actor, SlimeStateMachine stateMachine)
        {
            this.actor = actor;
            this.stateMachine = stateMachine;
        }

        public abstract void Enter();

        public abstract void Tick();
        
        public abstract void Exit();
    }
}