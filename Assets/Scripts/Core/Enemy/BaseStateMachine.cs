namespace Core.Enemy
{
    /// <summary>
    /// State machine for a simple <see cref="Enemy"/>.
    /// </summary>
    public class BaseStateMachine
    {
        /// <summary>
        /// The state that the enemy is currently in.
        /// </summary>
        public BaseState CurrentState { get; private set; }

        /// <summary>
        /// Sets starting state of the <see cref="BaseStateMachine"/> and makes it run.
        /// </summary>
        /// <param name="startingState">The state that <see cref="BaseStateMachine"/> should start in.</param>
        public void Initialize(BaseState startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        /// <summary>
        /// Changes state of the <see cref="BaseStateMachine"/> to <paramref name="newState"/>.
        /// </summary>
        /// <param name="newState">State to change <see cref="BaseStateMachine"/> into.</param>
        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit();

            CurrentState = newState;

            newState.Enter();
        }
    }
}