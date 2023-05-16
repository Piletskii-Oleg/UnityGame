using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onOpenMenu;
        [SerializeField] private UnityEvent onCloseMenu;

        private bool isMenuOpen;

        public void HandlePauseMenu()
        {
            if (isMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
        
        private void OpenMenu()
        {
            pauseMenu.SetActive(true);
            if (pauseMenu.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }

            isMenuOpen = true;
            
            onOpenMenu.Invoke();
        }

        public void CloseMenu()
        {
            if (pauseMenu.activeInHierarchy)
            {
                Time.timeScale = 1f;
            }
            
            pauseMenu.SetActive(false);

            isMenuOpen = false;
            
            onCloseMenu.Invoke();
        }

        public void Quit()
            => Application.Quit();
    }
}