using DataPersistence.DataFiles;
using UnityEngine;

namespace DataPersistence
{
    [CreateAssetMenu(menuName = "Managers/Options Data Manager")]
    public class OptionsDataManager : BaseDataManager<OptionsData, DataManager<OptionsData>>
    {
        [field: SerializeField] public float MouseSensitivity { get; set; }

        public override void Save()
        {
            storedData.mouseSensitivity = MouseSensitivity;
            
            base.Save();
        }

        public override void Load()
        {
            base.Load();

            MouseSensitivity = storedData.mouseSensitivity;
        }

        public void SetMouseSensitivity(string sensitivity)
            => MouseSensitivity = float.Parse(sensitivity);

        public void CreateNewConfig()
        {
            var data = new OptionsData
            {
                musicVolume = 0.5f,
                soundVolume = 0.5f,
                mouseSensitivity = 0.12f,
            };
            
            LoadFrom(data);
            Save();
        }
    }
}