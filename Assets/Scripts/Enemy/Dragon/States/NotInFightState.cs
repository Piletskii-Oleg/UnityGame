namespace Enemy.Dragon.States
{
    public class NotInFightState : DragonBaseState
    {
        public NotInFightState(BaseStateMachine stateMachine, Dragon dragon)
            : base(stateMachine, dragon)
        {
            dragon.AddState(this);
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

        public override void KillSequences()
        {
        }
    }
}