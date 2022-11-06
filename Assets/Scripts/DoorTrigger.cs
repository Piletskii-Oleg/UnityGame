using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator animator;

    private void Start()
        => animator = GetComponentInParent<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
        => animator.SetBool("character_nearby", false);
}
