using UnityEngine;

namespace ScriptableObjects.Guns
{
    /// <summary>
    /// Data for guns held by an entity in the world.
    /// </summary>
    [CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
    public class GunData : ScriptableObject
    {
        [Header("Info")]
        public new string name;

        [Header("Shooting")]
        public float damage;
        public float maxDistance; // currently unused
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

        [HideInInspector]
        public bool reloading;
    }
}
