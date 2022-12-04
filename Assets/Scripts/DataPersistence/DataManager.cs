using DataPersistence.DataFiles;
using UnityEngine;

namespace DataPersistence
{
    /// <summary>
    /// A scriptable object with data that should be saved.
    /// </summary>
    public abstract class DataManager : ScriptableObject
    {
        /// <summary>
        /// Saves class-specific data to the <see cref="GameData"/>.
        /// </summary>
        /// <param name="data">Game data to save to.</param>
        public abstract void SaveData(GameData data);
        
        /// <summary>
        /// Loads class-specific data from the <paramref name="data"/>
        /// </summary>
        /// <param name="data">Game data to load from.</param>
        public abstract void LoadData(GameData data);
    }
}