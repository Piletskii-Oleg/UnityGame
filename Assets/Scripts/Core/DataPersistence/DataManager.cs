using UnityEngine;

namespace Core.DataPersistence
{
    /// <summary>
    /// A scriptable object with data that should be saved.
    /// </summary>
    public abstract class DataManager<TData> : ScriptableObject
    {
        /// <summary>
        /// Saves class-specific data to the <see cref="TData"/>.
        /// </summary>
        /// <param name="data">Data to save to.</param>
        public abstract void SaveData(TData data);
        
        /// <summary>
        /// Loads class-specific data from the <see cref="TData"/>.
        /// </summary>
        /// <param name="data">Data to load from.</param>
        public abstract void LoadData(TData data);
    }
}