namespace Enemy.Slime.States
{
    public class AttackState : BaseState
    {
        public AttackState(Slime actor, SlimeStateMachine stateMachine)
            : base(actor, stateMachine)
        {
        }

        public override void Enter()
        {
            
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}