using DataPersistence.DataFiles;
using UnityEngine;

namespace EventListeners.GameEvents
{
    /// <inheritdoc cref="BaseGameEvent{T}"/>
    [CreateAssetMenu(menuName = "Game Event/OptionsData")]
    public class OptionsDataEvent : BaseGameEvent<OptionsData>
    {
    }
}