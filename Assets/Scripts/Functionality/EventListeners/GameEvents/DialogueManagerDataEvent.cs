using Core.EventListeners.GameEvents;
using Functionality.NPC.Dialogue;
using UnityEngine;

namespace Functionality.EventListeners.GameEvents
{
    /// <inheritdoc cref="BaseGameEvent{T}"/>.
    [CreateAssetMenu(menuName = "Game Event/DialogueManager")]
    public class DialogueManagerDataEvent : BaseGameEvent<DialogueManager>
    {
    }
}