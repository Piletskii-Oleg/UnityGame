using DataPersistence.GameDataFiles;
using UnityEngine;

namespace EventListeners.GameEvents
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Game Event/GameData")]
    public class GameDataEvent : BaseGameEvent<GameData>
    {
    }
}