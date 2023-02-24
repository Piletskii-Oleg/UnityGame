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
    }
}