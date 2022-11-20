using UnityEngine;

namespace Weapons.ScriptableObjects
{
    /// <summary>
    /// Base data for guns held by an entity in the world.
    /// </summary>
    [CreateAssetMenu(fileName = "Gun")]
    public class GunData : ScriptableObject
    {
        [Header("Info")]
        public string showName;

        [Header("Shooting")]
        public float damage;
        public float maxDistance;
        public bool canAutoShoot;
        
        [Header("Reloading")]
        public int currentAmmo;
        [Tooltip("The amount of ammo held in a single ammo magazine.")]
        public int ammoCapacity;
        [Tooltip("Fire rate of the gun measured in bullets per minute.")]
        public float fireRate;
        [Tooltip("Reload time of the gun measured in seconds.")]
        public float reloadTime;

        [Header("Scene object")]
        public GameObject gunPrefab;
    }
}
