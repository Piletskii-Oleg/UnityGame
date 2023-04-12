using Core.Enemy;

namespace Functionality.Enemy.Golem.States
{
    public abstract class GolemBaseState : BaseState
    {
        protected Golem golem;
        protected BaseStateMachine stateMachine;
        
        protected GolemBaseState(Golem golem, BaseStateMachine stateMachine)
        {
            this.golem = golem;
            this.stateMachine = stateMachine;
        }
    }
}