using System.Collections.Generic;
using UnityEngine;
using DataPersistence.GameDataFiles;
using UnityEngine.Events;

namespace DataPersistence
{
    /// <summary>
    /// Scriptable object that manages saving and loading from file.
    /// </summary>
    [CreateAssetMenu(fileName = "DataPersistenceManager", menuName = "Managers/Data Persistence Manager")]
    public class DataPersistenceManager : ScriptableObject
    {
        [Tooltip("File name to save or load from.")]
        [SerializeField] private string fileName;

        private GameData gameData;

        [Tooltip("Managers that deal with data that should be saved or loaded.")]
        [SerializeField] private List<DataManager> dataManagers;

        [SerializeField] private UnityEvent<GameData> onSaveGame;
        [SerializeField] private UnityEvent<GameData> onLoadGame;
        
        /// <summary>
        /// Invokes OnSaveGame event on all <see cref="MonoBehaviour"/>s it is attached to
        /// and saves data from all <see cref="DataManager"/>s.
        /// </summary>
        public void SaveGame()
        {
            onSaveGame.Invoke(gameData); // for MonoBehaviours
            
            foreach (var obj in dataManagers)
            {
                obj.SaveData(gameData);
            }

            FileDataHandler.Save(gameData, Application.persistentDataPath, fileName);
        }

        /// <summary>
        /// Invokes OnLoadGame event on all <see cref="MonoBehaviour"/>s it is attached to
        /// and loads data to all <see cref="DataManager"/>s.
        /// </summary>
        public void LoadGame()
        {
            gameData = FileDataHandler.Load(Application.persistentDataPath, fileName);
            
            foreach (var obj in dataManagers)
            {
                obj.LoadData(gameData);
            }

            onLoadGame.Invoke(gameData); // for MonoBehaviours
        }
    }
}