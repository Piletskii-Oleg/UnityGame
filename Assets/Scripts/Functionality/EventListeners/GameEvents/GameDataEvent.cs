using Core.DataPersistence.DataFiles;
using Core.EventListeners.GameEvents;
using UnityEngine;

namespace Functionality.EventListeners.GameEvents
{
    /// <inheritdoc cref="BaseGameEvent{T}"/>.
    [CreateAssetMenu(fileName = "GameData", menuName = "Game Event/GameData")]
    public class GameDataEvent : BaseGameEvent<GameData>
    {
    }
}