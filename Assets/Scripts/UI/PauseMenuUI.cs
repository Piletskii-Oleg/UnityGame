using UnityEngine;

namespace UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

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
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

            isMenuOpen = true;
        }

        public void CloseMenu()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;

            isMenuOpen = false;
        }

        public void Quit()
            => Application.Quit();
    }
}