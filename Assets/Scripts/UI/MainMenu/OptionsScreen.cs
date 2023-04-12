using DataPersistence;
using DataPersistence.DataFiles;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class OptionsScreen : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private MusicManager musicManager;
        [FormerlySerializedAs("optionsManager")]
        [SerializeField] private OptionsDataManager optionsDataManager;
    
        [Header("Music and Sound")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider soundSlider;
    
        [Header("Sensitivity")]
        [SerializeField] private GameObject sensitivityPlaceholder;

        private void OnEnable()
        {
            musicSlider.value = musicManager.MusicVolume;
            soundSlider.value = musicManager.SoundVolume;
        
            sensitivityPlaceholder.GetComponent<TextMeshProUGUI>().text = optionsDataManager.MouseSensitivity.ToString();
        }
    }
}
