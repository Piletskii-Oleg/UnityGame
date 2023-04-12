using Core.EventListeners.GameEvents;
using UnityEngine;

namespace Functionality.EventListeners.GameEvents
{
    /// <inheritdoc/>
    [CreateAssetMenu(fileName = "string", menuName = "Game Event/string")]
    public class StringGameEvent : BaseGameEvent<string>
    {
    }
}
