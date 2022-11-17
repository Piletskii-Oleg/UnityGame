using UnityEngine.Events;

/// <summary>
/// An abstraction over <see cref="UnityEvent"/> with one generic parameter <typeparamref name="T"/> that allows raising it independently.
/// </summary>
/// <typeparam name="T">Parameter type passed to the <see cref="BaseGameEvent{T}"/>.</typeparam>
public interface IEventListener<T>
{
    /// <summary>
    /// Called when the attached <see cref="GameEvent"/> is raised.
    /// </summary>
    void OnEventRaised(T parameter);
}
