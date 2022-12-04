using System.Collections.Generic;
using DataPersistence.DataFiles;
using UnityEngine;
using UnityEngine.Events;

namespace DataPersistence
{
    [CreateAssetMenu(menuName = "Managers/Options Manager")]
    public class OptionsManager : ScriptableObject
    {
        [field: SerializeField] public OptionsData OptionsData { get; private set; }

        [SerializeField] private List<OptionsDataManager> dataManagers;
        
        [Header("OptionsData Events")]
        [SerializeField] private UnityEvent<OptionsData> onSaveOptions;
        [SerializeField] private UnityEvent<OptionsData> onLoadOptions;

        public void SaveOptions()
        {
            foreach (var manager in dataManagers)
            {
                manager.SaveOptions(OptionsData);
            }

            onSaveOptions.Invoke(OptionsData);
            
            FileDataHandler.Save(OptionsData, Application.persistentDataPath, "options.json");
        }

        public void LoadOptions()
        {
            OptionsData = FileDataHandler.Load<OptionsData>(Application.persistentDataPath, "options.json") as OptionsData;
            
            foreach (var manager in dataManagers)
            {
                manager.LoadOptions(OptionsData);
            }

            onLoadOptions.Invoke(OptionsData);
        }

        public void SetMouseSensitivity(string sensitivity)
            => OptionsData.mouseSensitivity = float.Parse(sensitivity.ToString());
    }
}