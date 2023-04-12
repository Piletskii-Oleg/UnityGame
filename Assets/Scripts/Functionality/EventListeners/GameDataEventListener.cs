using Core.DataPersistence.DataFiles;
using Core.EventListeners;
using Functionality.EventListeners.GameEvents;
using UnityEngine.Events;

namespace Functionality.EventListeners
{
    /// <summary>
    /// <see cref="BaseGameEventListener{T, TGameEvent, TUnityEvent}"/> based on <see cref="GameData"/>.
    /// </summary>
    public class GameDataEventListener : BaseGameEventListener<GameData, GameDataEvent, UnityEvent<GameData>>
    {
    }
}