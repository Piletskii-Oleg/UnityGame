namespace Enemy.Slime.States
{
    public class SlimeStateMachine
    {
        public BaseState CurrentState { get; private set; }
        
        public void Initialize(BaseState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }
        
        public void ChangeState(BaseState newState)
        {
            CurrentState = newState;
            newState.Enter();
        }
    }
}