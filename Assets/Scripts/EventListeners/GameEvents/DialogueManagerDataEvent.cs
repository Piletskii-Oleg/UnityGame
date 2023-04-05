using NPC.Dialogue;
using UnityEngine;

namespace EventListeners.GameEvents
{
    /// <inheritdoc cref="BaseGameEvent{T}"/>.
    [CreateAssetMenu(menuName = "Game Event/DialogueManager")]
    public class DialogueManagerDataEvent : BaseGameEvent<DialogueManager>
    {
    }
}