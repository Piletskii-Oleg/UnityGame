using UnityEngine;

namespace Core.DataPersistence
{
    /// <summary>
    /// Interface for <see cref="MonoBehaviour"/>s that should save or load data.
    /// </summary>
    public interface IDataPersistence<in TData>
    {
        /// <summary>
        /// Invoked when data is saved. Saves its specific data to the <see cref="TData"/> class.
        /// </summary>
        /// <param name="data"><see cref="TData"/> that stores information about the game session.</param>
        void OnSave(TData data);

        /// <summary>
        /// Invoked when data is loaded. Loads its specific data from the <see cref="TData"/> class.
        /// </summary>
        /// <param name="data"><see cref="TData"/> that stores information about the game session.</param>
        void OnLoad(TData data);
    }
}