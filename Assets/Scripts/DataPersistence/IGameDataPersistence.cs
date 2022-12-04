using DataPersistence.DataFiles;
using UnityEngine;

namespace DataPersistence
{
    /// <summary>
    /// Interface for <see cref="MonoBehaviour"/>s that should save or load data.
    /// </summary>
    public interface IGameDataPersistence
    {
        /// <summary>
        /// Invoked when data is saved. Saves its specific data to the <see cref="GameData"/> class.
        /// </summary>
        /// <param name="data"><see cref="GameData"/> that stores information about the game session.</param>
        void OnSave(GameData data);

        /// <summary>
        /// Invoked when data is loaded. Loads its specific data from the <see cref="GameData"/> class.
        /// </summary>
        /// <param name="data"><see cref="GameData"/> that stores information about the game session.</param>
        void OnLoad(GameData data);
    }
}