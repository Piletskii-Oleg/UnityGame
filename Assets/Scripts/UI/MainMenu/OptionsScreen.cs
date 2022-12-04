using DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class OptionsScreen : MonoBehaviour
    {
        [Header("Options Manager")]
        [SerializeField] private OptionsManager manager;
    
        [Header("Music and Sound")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider soundSlider;
    
        [Header("Sensitivity")]
        [SerializeField] private GameObject sensitivityPlaceholder;

        private void OnEnable()
        {
            musicSlider.value = manager.OptionsData.musicVolume;
            soundSlider.value = manager.OptionsData.soundVolume;
        
            sensitivityPlaceholder.GetComponent<TextMeshProUGUI>().text = manager.OptionsData.mouseSensitivity.ToString();
        }
    }
}
