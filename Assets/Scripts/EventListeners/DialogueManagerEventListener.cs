using EventListeners.GameEvents;
using NPC.Dialogue;
using UnityEngine.Events;

namespace EventListeners
{
    /// <summary>
    /// <see cref="BaseGameEventListener{T, TGameEvent, TUnityEvent}"/> based on <see cref="DialogueManager"/>.
    /// </summary>
    public class DialogueManagerEventListener : BaseGameEventListener<DialogueManager,
        BaseGameEvent<DialogueManager>,
        UnityEvent<DialogueManager>>
    {
    }
}
