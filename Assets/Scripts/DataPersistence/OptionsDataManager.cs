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
            data.mouseSensitivity = MouseSensitivity;
            
            base.Save();
        }

        public override void Load()
        {
            base.Load();

            MouseSensitivity = data.mouseSensitivity;
        }

        public void SetMouseSensitivity(string sensitivity)
            => MouseSensitivity = float.Parse(sensitivity);
    }
}