using DataPersistence.GameDataFiles;
using EventListeners.GameEvents;
using UnityEngine.Events;

namespace EventListeners
{
    public class GameDataEventListener : BaseGameEventListener<GameData, GameDataEvent, UnityEvent<GameData>>
    {
    }
}