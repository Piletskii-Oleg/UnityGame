using UnityEngine;

namespace Enemy.Slime.States
{
    public abstract class BaseState
    {
        protected Texture stateFace;
        protected Slime actor;
        protected SlimeStateMachine stateMachine;

        protected BaseState(Slime actor, SlimeStateMachine stateMachine, Texture stateFace)
        {
            this.actor = actor;
            this.stateMachine = stateMachine;
            this.stateFace = stateFace;
        }

        public virtual void Enter()
        {
            actor.SetFace(stateFace);
        }

        public abstract void Tick();
        
        public abstract void Exit();
    }
}