using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public bool canAutoShoot;

    [Header("Reloading")]
    public int currentAmmo;
    public int ammoCapacity;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;

}
