using Shared;
using Shared.ScriptableObjects;
using UnityEngine;
using Weapons.ScriptableObjects;

namespace Weapons
{
    /// <summary>
    /// Class used for bullets shot by weapons.
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        private float damage;
        
        [SerializeField] private WeaponManager weaponManager;
        
        [Tooltip("Speed with which bullet is shot.")]
        [SerializeField] private float thrust;
        
        [Tooltip("Affiliation of the actor who shot the bullet")]
        [SerializeField]
        private ActorAffiliation affiliation;

        private float timeSinceCreation;
        
        [Tooltip("Max time that a bullet can exist for.")]
        [SerializeField] private float maxTime;

        private Rigidbody rigidBody;

        private void Start()
        {
            damage = weaponManager.CurrentGunData.damage;
            rigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rigidBody.AddForce(thrust * Time.fixedDeltaTime * transform.up, ForceMode.VelocityChange);
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
                actor.OnTakeDamage(damage, affiliation);
            }
            else if (collision.gameObject.TryGetComponent<HealthSurrogate>(out var health))
            {
                health.OnTakeDamage(damage, affiliation);
            }

            Destroy(gameObject);
        }
    }
}
