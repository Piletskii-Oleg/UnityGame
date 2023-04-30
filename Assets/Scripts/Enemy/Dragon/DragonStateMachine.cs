namespace Enemy.Dragon
{
    public class DragonStateMachine : BaseStateMachine
    {
        private readonly Dragon dragon;

        public DragonStateMachine(Dragon dragon)
        {
            this.dragon = dragon;
        }
        
        public override void ChangeState(BaseState newState)
        {
            dragon.StopEruptingFlames();
            
            if (CurrentState != dragon.PlayerRanAwayState)
            {
                base.ChangeState(newState);
            }
        }
    }
}