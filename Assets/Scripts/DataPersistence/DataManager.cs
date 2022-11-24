using DataPersistence.GameDataFiles;
using UnityEngine;

namespace DataPersistence
{
    public abstract class DataManager : ScriptableObject
    {
        public abstract void SaveData(GameData data);
        
        public abstract void LoadData(GameData data);
    }
}