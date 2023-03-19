﻿using Enemy.Slime.States;

namespace Enemy
{
    public class BaseStateMachine
    {
        /// <summary>
        /// The state that the slime is currently in.
        /// </summary>
        public BaseState CurrentState { get; private set; }

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