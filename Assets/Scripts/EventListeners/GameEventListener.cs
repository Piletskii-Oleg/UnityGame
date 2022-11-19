using ScriptableObjects.GameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace EventListeners
{
    /// <summary>
    /// An abstraction over <see cref="UnityEvent"/> that allows raising <see cref="UnityEvent"/> independently.
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private GameEvent gameEvent;

        [SerializeField]
        [Tooltip("Response to invoke when Game Event is raised.")]
        private UnityEvent response;

        /// <summary>
        /// Called when the attached <see cref="GameEvent"/> is raised.
        /// </summary>
        public void OnEventRaised()
            => response.Invoke();

        private void OnEnable()
            => gameEvent.RegisterListener(this);

        private void OnDisable()
            => gameEvent.UnregisterListener(this);
    }
}