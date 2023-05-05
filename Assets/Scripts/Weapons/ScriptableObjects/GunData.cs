using UnityEngine;

namespace Weapons.ScriptableObjects
{
    /// <summary>
    /// Base data for guns held by an entity in the world.
    /// </summary>
    [CreateAssetMenu(menuName = "Data/Gun Data")]
    public class GunData : ScriptableObject
    {
        [Header("Info")]
        public string showName;

        [Header("Shooting")]
        public float damage;
        public float maxDistance;
        public bool canAutoShoot;

        [Header("Total count")]
        [Tooltip("Total amount of ammo that the player is carrying")]
        public int currentTotalAmmo;
        [Tooltip("Total amount of ammo that the player can carry")]
        public int totalAmmo;

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
