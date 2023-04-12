using Core.DataPersistence.DataFiles;
using Core.EventListeners.GameEvents;
using UnityEngine;

namespace Functionality.EventListeners.GameEvents
{
    /// <inheritdoc cref="BaseGameEvent{T}"/>
    [CreateAssetMenu(menuName = "Game Event/OptionsData")]
    public class OptionsDataEvent : BaseGameEvent<OptionsData>
    {
    }
}