using System.Collections.Generic;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

namespace DataPersistence
{
    public abstract class BaseDataManager<TData, TManager> : ScriptableObject
        where TManager : DataManager<TData>
        where TData : class
    {
        [Tooltip("Managers that deal with storedData that should be saved or loaded.")]
        [SerializeField] protected List<TManager> dataManagers;

        [Tooltip("Name of the file where info is saved")]
        [SerializeField] protected string fileName;
        
        protected TData storedData;
        
        [Header("Events")]
        [SerializeField] protected UnityEvent<TData> onSaveGame;
        [SerializeField] protected UnityEvent<TData> onLoadGame;
        
        /// <summary>
        /// Invokes OnSaveGame event on all <see cref="MonoBehaviour"/>s it is attached to
        /// and saves data from all <see cref="DataManager{TData}"/>.
        /// </summary>
        public virtual void Save()
        {
            onSaveGame.Invoke(storedData); // for MonoBehaviours
            
            foreach (var obj in dataManagers)
            {
                obj.SaveData(storedData);
            }

            FileDataHandler.Save(storedData, Application.persistentDataPath, fileName);
        }

        public virtual void Load()
            => LoadFrom(FileDataHandler.Load<TData>(Application.persistentDataPath, fileName));

        /// <summary>
        /// Invokes OnLoadGame event on all <see cref="MonoBehaviour"/>s it is attached to
        /// and loads stored data to all <see cref="DataManager{TData}"/>.
        /// </summary>
        protected void LoadFrom(TData data)
        {
            storedData = data;
            
            foreach (var obj in dataManagers)
            {
                obj.LoadData(storedData);
            }
            
            LoadMiscellaneous();

            onLoadGame.Invoke(storedData); // for MonoBehaviours
        }

        /// <summary>
        /// A helper method to load something before applying it to the scene objects
        /// which happens on invocation of <see cref="onLoadGame"/>.
        /// </summary>
        protected virtual void LoadMiscellaneous()
        {
        }
        
        public bool CheckForSave()
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(Application.persistentDataPath, fileName)))
            {
                return true;
            }

            return false;
        }
    }
}