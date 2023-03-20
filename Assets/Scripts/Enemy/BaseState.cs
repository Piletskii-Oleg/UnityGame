namespace Enemy
{
    /// <summary>
    /// Base state of an actor from which all other inherit.
    /// </summary>
    public abstract class BaseState
    {
        /// <summary>
        /// Called when actor enters the state.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// Called every frame when actor is in the state.
        /// </summary>
        public abstract void Tick();

        /// <summary>
        /// Called when actor leaves the state.
        /// </summary>
        public abstract void Exit();
    }
}