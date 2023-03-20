using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        
        [SerializeField] private Slider slider;
        [SerializeField] [Range(0f, 75f)] private float maxDistance;

        private void Update()
        {
            var playerPosition = playerTransform.position;
            if (Vector3.Distance(playerPosition, transform.position) < maxDistance)
            {
                slider.gameObject.SetActive(true);
                slider.transform.LookAt(playerPosition);
            }
            else
            {
                slider.gameObject.SetActive(false);
            }
        }
    }
}