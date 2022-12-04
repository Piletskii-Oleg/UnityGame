using DataPersistence.DataFiles;
using UnityEngine;

namespace DataPersistence
{
    public abstract class OptionsDataManager : ScriptableObject
    {
        public abstract void SaveOptions(OptionsData data);
        
        public abstract void LoadOptions(OptionsData data);
    }
}