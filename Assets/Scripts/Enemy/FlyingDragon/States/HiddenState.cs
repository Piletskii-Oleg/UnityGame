namespace Enemy.FlyingDragon.States
{
    /// <summary>
    /// State that corresponds to the dragon being hidden underground.
    /// </summary>
    public class HiddenState : FlyingBaseState
    {
        public HiddenState(FlyingDragon dragon, BaseStateMachine stateMachine)
            : base(dragon, stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}