using System.Globalization;
using DataPersistence;
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

            var textField = sensitivityPlaceholder.GetComponent<TextMeshProUGUI>();
            textField.text = optionsDataManager.MouseSensitivity.ToString(CultureInfo.InvariantCulture);
        }
    }
}
