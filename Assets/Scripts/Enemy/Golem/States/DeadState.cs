namespace Enemy.Golem.States
{
    public class DeadState : GolemBaseState
    {
        public DeadState(Golem golem, BaseStateMachine stateMachine)
            : base(golem, stateMachine)
        {
        }

        public override void Enter()
        {
            golem.Stop();
        }

        public override void Tick()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}