using UnityEngine;
using UnityEngine.Events;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private int damage;

    private float timeSinceCreation;
    [SerializeField] private float maxTime;

    private void FixedUpdate()
    {
        transform.Translate(-thrust * Time.fixedDeltaTime * transform.up, Space.World);
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
