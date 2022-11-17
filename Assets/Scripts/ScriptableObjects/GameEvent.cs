using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An abstraction over <see cref="UnityEvent"/> that allows for multiple listeners using <see cref="GameEventListener"/>.
/// </summary>
[CreateAssetMenu(fileName = "No type", menuName = "Game Event/No type")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> listeners = new ();

    /// <summary>
    /// Called when the event is raised.
    /// Raises all listeners starting from the last one added.
    /// </summary>
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    /// <summary>
    /// Adds listener if it has not been added yet.
    /// Does not require manual call.
    /// It is automatically used when <see cref="GameEventListener"/> is put in <see cref="UnityEvent"/>.
    /// </summary>
    /// <param name="listener"><see cref="GameEventListener"/> put on a <see cref="GameObject"/> in the world.</param>
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    /// <summary>
    /// Removes listener if it has been added.
    /// </summary>
    /// <param name="listener"><see cref="GameEventListener"/> put on a <see cref="GameObject"/> in the world.</param>
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
