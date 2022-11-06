using UnityEngine;

/// <summary>
/// Class used for bullets shot by weapons.
/// </summary>
public class Bullet : MonoBehaviour
{
    [Tooltip("Speed with which bullet is shot.")]
    [SerializeField] private float thrust;
    [Tooltip("Damage that bullet deals.")]
    [SerializeField] private int damage;

    private float timeSinceCreation;
    [Tooltip("Max time that a bullet can exist for.")]
    [SerializeField] private float maxTime;

    private void FixedUpdate()
    {
        transform.Translate(-thrust * Time.fixedDeltaTime * transform.up, Space.World); // thrust is negative because otherwise bullet is shot at player and not forward.
        timeSinceCreation += Time.fixedDeltaTime;

        if (timeSinceCreation > maxTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Actor>(out var actor))
        {
            actor.OnTakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
