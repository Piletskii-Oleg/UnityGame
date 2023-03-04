using UnityEngine;

namespace Enemy.Slime.States
{
    /// <summary>
    /// Base state of an actor from which all other inherit.
    /// </summary>
    public abstract class BaseState
    {
        private readonly Texture stateFace;
        protected readonly Slime actor;
        protected readonly SlimeStateMachine stateMachine;

        protected BaseState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
        {
            this.actor = actor;
            this.stateMachine = stateMachine;
            this.stateFace = stateFace;
        }

        /// <summary>
        /// Called when actor enters the state.
        /// </summary>
        public virtual void Enter()
        {
            actor.SetFace(stateFace);
        }

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