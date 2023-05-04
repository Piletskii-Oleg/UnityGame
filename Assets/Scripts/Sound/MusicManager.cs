using DataPersistence;
using DataPersistence.DataFiles;
using UnityEngine;
using UnityEngine.Events;

namespace Sound
{
    /// <summary>
    /// Stores and manages information about music and sound effects.
    /// </summary>
    [CreateAssetMenu(fileName = "Music Manager", menuName = "Managers/Music Manager")]
    public class MusicManager : DataManager<OptionsData>
    {
        [field: Range(0, 1f)]
        [field:SerializeField] public float MusicVolume { get; set; }
        
        [field: Range(0, 1f)]
        [field:SerializeField] public float SoundVolume { get; set; }

        [SerializeField] private UnityEvent<float> onSoundVolumeChanged;

        public override void LoadData(OptionsData data)
        {
            MusicVolume = data.musicVolume;
            SoundVolume = data.soundVolume;
            
            onSoundVolumeChanged.Invoke(SoundVolume);
        }

        public override void SaveData(OptionsData data)
        {
            data.musicVolume = MusicVolume;
            data.soundVolume = SoundVolume;
        }
    }
}