using Core.DataPersistence.DataFiles;
using UnityEngine;

namespace Core.DataPersistence
{
    /// <summary>
    /// Scriptable object that manages saving and loading from file.
    /// </summary>
    [CreateAssetMenu(menuName = "Managers/Game Data Manager")]
    public class GameDataManager : BaseDataManager<GameData, DataManager<GameData>>
    {
        public void NewGame()
        {
            storedData = new GameData();
            Save();
        }
    }
}