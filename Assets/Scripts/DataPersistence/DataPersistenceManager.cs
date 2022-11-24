using System;
using System.Collections.Generic;
using UnityEngine;
using DataPersistence.GameDataFiles;
using UnityEngine.Events;

namespace DataPersistence
{
    [CreateAssetMenu(fileName = "DataPersistenceManager", menuName = "Managers/Data Persistence Manager", order = 0)]
    public class DataPersistenceManager : ScriptableObject
    {
        [SerializeField] private string fileName;

        private GameData gameData;

        [SerializeField] private List<DataManager> dataManagers;

        [SerializeField] private UnityEvent<GameData> onSaveGame;
        [SerializeField] private UnityEvent<GameData> onLoadGame; 

        public void NewGame()
            => this.gameData = new GameData();

        public void SaveGame()
        {
            onSaveGame.Invoke(gameData); // for MonoBehaviours
            
            foreach (var obj in dataManagers)
            {
                obj.SaveData(gameData);
            }

            FileDataHandler.Save(gameData, Application.persistentDataPath, fileName);
        }

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