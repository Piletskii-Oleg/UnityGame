namespace Enemy.Slime.States
{
    /// <summary>
    /// State machine that changes slime's states.
    /// </summary>
    public class SlimeStateMachine : BaseStateMachine
    {
        private SlimeFacesList facesList;

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
    }
}