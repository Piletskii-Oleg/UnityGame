namespace Enemy.Slime.States
{
    /// <summary>
    /// State machine that changes slime's states.
    /// </summary>
    public class SlimeStateMachine
    {
        private SlimeFacesList facesList;
        
        /// <summary>
        /// The state that the slime is currently in.
        /// </summary>
        public BaseState CurrentState { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimeStateMachine"/> class.
        /// </summary>
        /// <param name="facesList">
        /// A <see cref="SlimeFacesList"/> scriptable object
        /// that contains references to all possible faces.
        /// </param>
        public SlimeStateMachine(SlimeFacesList facesList)
        {
            this.facesList = facesList;
        }
        
        /// <summary>
        /// Sets starting state of the <see cref="SlimeStateMachine"/> and makes it run.
        /// </summary>
        /// <param name="startingState">The state that <see cref="SlimeStateMachine"/> should start in.</param>
        public void Initialize(BaseState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }
        
        /// <summary>
        /// Changes state of the <see cref="SlimeStateMachine"/> to <paramref name="newState"/>.
        /// </summary>
        /// <param name="newState">State to change <see cref="SlimeStateMachine"/> into.</param>
        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();
            
            CurrentState = newState;
            
            newState.Enter();
        }
    }
}