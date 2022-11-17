using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for all generic event listeners. Inherits from <see cref="IEventListener{T}"/>.
/// </summary>
/// <typeparam name="T">Parameter type passed to the <see cref="BaseGameEvent{T}"/></typeparam>
/// <typeparam name="TGameEvent">Game Event that you subscribe to.</typeparam>
/// <typeparam name="TUnityEvent">Response to invoke when <see cref="BaseGameEvent{T}"/> is called.</typeparam>
public class BaseGameEventListener<T, TGameEvent, TUnityEvent> : MonoBehaviour, IEventListener<T>
    where TGameEvent : BaseGameEvent<T>
    where TUnityEvent : UnityEvent<T>
{
    [SerializeField]
    [Tooltip("Event to register with.")]
    private TGameEvent gameEvent;

    [SerializeField]
    [Tooltip("Response to invoke when Game Event is raised.")]
    private TUnityEvent response;

    /// <summary>
    /// Called when the attached <see cref="BaseGameEvent{T}"/> is raised.
    /// </summary>
    public void OnEventRaised(T parameter)
        => response.Invoke(parameter);

    private void OnEnable()
        => gameEvent.RegisterListener(this);

    private void OnDisable()
        => gameEvent.UnregisterListener(this);
}
