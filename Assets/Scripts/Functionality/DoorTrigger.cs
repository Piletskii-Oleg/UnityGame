using UnityEngine;

namespace Functionality
{
    /// <summary>
    /// Class for a door that opens when the player gets close.
    /// </summary>
    public class DoorTrigger : MonoBehaviour
    {
        private Animator animator;
        private static readonly int CharacterNearby = Animator.StringToHash("character_nearby");

        private void Start()
            => animator = GetComponentInParent<Animator>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                animator.SetBool(CharacterNearby, true);
            }
        }

        private void OnTriggerExit(Collider other)
            => animator.SetBool(CharacterNearby, false);
    }
}
