using DataPersistence.DataFiles;
using EventListeners.GameEvents;
using UnityEngine.Events;

namespace EventListeners
{
    /// <summary>
    /// <see cref="BaseGameEventListener{T, TGameEvent, TUnityEvent}"/> based on <see cref="GameData"/>.
    /// </summary>
    public class GameDataEventListener : BaseGameEventListener<GameData, GameDataEvent, UnityEvent<GameData>>
    {
    }
}