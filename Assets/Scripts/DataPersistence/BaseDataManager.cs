using System.Collections.Generic;
using DataPersistence.DataFiles;
using UnityEngine;
using UnityEngine.Events;

namespace DataPersistence
{
    public abstract class BaseDataManager<TData, TManager> : ScriptableObject
        where TManager : DataManager<TData>
        where TData : class
    {
        [Tooltip("Managers that deal with data that should be saved or loaded.")]
        [SerializeField] protected List<TManager> dataManagers;

        [Tooltip("Name of the file where info is saved")]
        [SerializeField] protected string fileName;
        
        protected TData data;
        
        [Header("Events")]
        [SerializeField] protected UnityEvent<TData> onSaveGame;
        [SerializeField] protected UnityEvent<TData> onLoadGame;
        
        /// <summary>
        /// Invokes OnSaveGame event on all <see cref="MonoBehaviour"/>s it is attached to
        /// and saves data from all <see cref="DataManager{TData}"/>.
        /// </summary>
        public virtual void Save()
        {
            onSaveGame.Invoke(data); // for MonoBehaviours
            
            foreach (var obj in dataManagers)
            {
                obj.SaveData(data);
            }

            FileDataHandler.Save(data, Application.persistentDataPath, fileName);
        }
        
        /// <summary>
        /// Invokes OnLoadGame event on all <see cref="MonoBehaviour"/>s it is attached to
        /// and loads data to all <see cref="DataManager{TData}"/>.
        /// </summary>
        public virtual void Load()
        {
            data = FileDataHandler.Load<TData>(Application.persistentDataPath, fileName) as TData;
            
            foreach (var obj in dataManagers)
            {
                obj.LoadData(data);
            }

            onLoadGame.Invoke(data); // for MonoBehaviours
        }
    }
}