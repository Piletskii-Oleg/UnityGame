using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// Base state of a slime from which all other inherit.
    /// </summary>
    public abstract class SlimeBaseState : BaseState
    {
        private readonly Texture stateFace;
        protected readonly Slime slime;
        protected readonly BaseStateMachine stateMachine;

        protected SlimeBaseState(Slime slime, BaseStateMachine stateMachine, Texture stateFace)
        {
            this.slime = slime;
            this.stateMachine = stateMachine;
            this.stateFace = stateFace;
        }

        /// <summary>
        /// Called when actor enters the state.
        /// </summary>
        public override void Enter()
        {
            slime.SetFace(stateFace);
        }
    }
}