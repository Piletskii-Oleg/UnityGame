using Core.Player.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Functionality.UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private Transform playerTransform;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        
        [SerializeField] private Slider slider;
        [SerializeField] [Range(0f, 75f)] private float maxDistance;

        private void Start()
        {
            if (!playerTransform)
            {
                playerTransform = playerScriptableObject.GetActualPlayerTransform();
            }
        }

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