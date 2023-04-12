using Core.EventListeners;
using Core.EventListeners.GameEvents;
using Functionality.NPC.Dialogue;
using UnityEngine.Events;

namespace Functionality.EventListeners
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
