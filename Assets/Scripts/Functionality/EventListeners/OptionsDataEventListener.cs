using Core.DataPersistence.DataFiles;
using Core.EventListeners;
using Functionality.EventListeners.GameEvents;
using UnityEngine.Events;

namespace Functionality.EventListeners
{
    public class OptionsDataEventListener : BaseGameEventListener<OptionsData, OptionsDataEvent, UnityEvent<OptionsData>>
    {
    }
}