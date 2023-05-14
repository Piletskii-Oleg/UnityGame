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
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

            isMenuOpen = true;
            
            onOpenMenu.Invoke();
        }

        public void CloseMenu()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;

            isMenuOpen = false;
            
            onCloseMenu.Invoke();
        }

        public void Quit()
            => Application.Quit();
    }
}