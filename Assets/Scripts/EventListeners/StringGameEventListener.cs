using ScriptableObjects.GameEvents;
using UnityEngine.Events;

namespace EventListeners
{
    /// <summary>
    /// <see cref="BaseGameEventListener{T, TGameEvent, TUnityEvent}"/> based on <see cref="System.String"/>.
    /// </summary>
    public class StringGameEventListener : BaseGameEventListener<string, BaseGameEvent<string>, UnityEvent<string>>
    {
    }
}
