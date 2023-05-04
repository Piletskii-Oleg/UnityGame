using EventListeners.GameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace EventListeners
{
    public class AudioClipEventListener : BaseGameEventListener<AudioClip, AudioClipEvent, UnityEvent<AudioClip>>
    {
    }
}