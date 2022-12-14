using DataPersistence.DataFiles;
using UnityEngine;

namespace DataPersistence
{
    /// <summary>
    /// Scriptable object that manages saving and loading from file.
    /// </summary>
    [CreateAssetMenu(menuName = "Managers/Game Data Manager")]
    public class GameDataManager : BaseDataManager<GameData, DataManager<GameData>>
    {
        public void NewGame()
        {
            data = new GameData();
            Save();
        }
    }
}