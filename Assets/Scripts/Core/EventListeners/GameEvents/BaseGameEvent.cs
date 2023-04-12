using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.EventListeners.GameEvents
{
    /// <summary>
    /// An abstraction over <see cref="UnityEvent"/> with one generic parameter <typeparamref name="T"/>
    /// that allows for multiple listeners using <see cref="BaseGameEventListener{T, TGameEvent, TUnityEvent}"/>.
    /// </summary>
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IEventListener<T>> listeners = new ();

        /// <summary>
        /// Called when the event is raised.
        /// Raises all listeners starting from the last one added.
        /// </summary>
        public void Raise(T parameter)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(parameter);
            }
        }
    
        /// <summary>
        /// Adds listener if it has not been added yet.
        /// Does not require manual call.
        /// It is automatically used when <see cref="GameEventListener"/> is put in <see cref="UnityEvent"/>.
        /// </summary>
        /// <param name="listener"><see cref="GameEventListener"/> put on a <see cref="GameObject"/> in the world.</param>
        public void RegisterListener(IEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        /// <summary>
        /// Removes listener if it has been added.
        /// </summary>
        /// <param name="listener"><see cref="IEventListener"/> put on a <see cref="GameObject"/> in the world.</param>
        public void UnregisterListener(IEventListener<T> listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}
