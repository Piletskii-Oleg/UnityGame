using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeathUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private GameObject buttons;

        public void OnDeath()
        {
            StartCoroutine(DrawBlood());
        }

        public void Quit()
            => Application.Quit();

        private IEnumerator DrawBlood()
        {
            image.gameObject.SetActive(true);
            
            var color = image.color;
            color.a = 0;
            image.color = color;
            
            while (color.a < 1)
            {
                color.a += 0.03f;
                image.color = color;
                yield return null;
            }
            
            buttons.SetActive(true);
        }
    }
}