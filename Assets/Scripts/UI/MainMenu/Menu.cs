using System;
using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private DataPersistenceManager dataManager;
        [SerializeField] private OptionsManager optionsManager;
        
        public void StartNewGame()
        {
            dataManager.NewGame();
            SceneManager.LoadScene("World");
        }

        public void Continue()
        {
            SceneManager.LoadScene("World", LoadSceneMode.Single);
        }

        private void OnEnable()
        {
            optionsManager.LoadOptions();
        }
    }
}