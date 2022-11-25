using DataPersistence.GameDataFiles;
using UnityEngine;

namespace EventListeners.GameEvents
{
    /// <inheritdoc cref="BaseGameEvent{T}"/>.
    [CreateAssetMenu(fileName = "GameData", menuName = "Game Event/GameData")]
    public class GameDataEvent : BaseGameEvent<GameData>
    {
    }
}