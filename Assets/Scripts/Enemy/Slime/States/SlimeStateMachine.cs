namespace Enemy.Slime.States
{
    public class SlimeStateMachine
    {
        private SlimeFacesList facesList;
        
        public BaseState CurrentState { get; private set; }

        public SlimeStateMachine(SlimeFacesList facesList)
        {
            this.facesList = facesList;
        }
        
        public void Initialize(BaseState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }
        
        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();
            
            CurrentState = newState;
            
            newState.Enter();
        }
    }
}