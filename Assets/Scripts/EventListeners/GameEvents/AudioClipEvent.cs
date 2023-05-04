using UnityEngine;

namespace EventListeners.GameEvents
{
    /// <inheritdoc cref="BaseGameEvent{T}"/>.
    [CreateAssetMenu(menuName = "Game Event/AudioClip")]
    public class AudioClipEvent : BaseGameEvent<AudioClip>
    {
    }
}