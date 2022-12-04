using DataPersistence.DataFiles;
using EventListeners.GameEvents;
using UnityEngine.Events;

namespace EventListeners
{
    public class OptionsDataEventListener : BaseGameEventListener<OptionsData, OptionsDataEvent, UnityEvent<OptionsData>>
    {
    }
}