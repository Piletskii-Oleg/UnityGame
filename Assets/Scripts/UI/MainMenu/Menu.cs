using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI.MainMenu
{
    public class Menu : MonoBehaviour
    {
        [FormerlySerializedAs("optionsManager")]
        [SerializeField] private OptionsDataManager optionsDataManager;
        
        public void StartNewGame()
        {
           // gameDataManager.NewGame();
            SceneManager.LoadScene("World");
        }

        public void Continue()
        {
            SceneManager.LoadScene("World", LoadSceneMode.Single);
        }

        private void OnEnable()
        {
            optionsDataManager.Load();
        }
    }
}