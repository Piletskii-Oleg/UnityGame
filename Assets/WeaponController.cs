using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private Transform weaponHolder;

    private Weapon currentWeapon;

    private void Start()
    {
        this.ChangeWeapon(1);
    }

    /// <summary>
    /// Makes the player shoot with the currently held weapon.
    /// </summary>
    public void Shoot()
        => currentWeapon.Shoot();

    /// <summary>
    /// Makes the player start reloading the currently held weapon.
    /// </summary>
    public void StartReload()
        => currentWeapon.StartReload();

    /// <summary>
    /// Used to change the currently held weapon.
    /// </summary>
    /// <param name="index">Index of the weapon in the list.</param>
    public void ChangeWeapon(int index)
    {
        index--; // index parameter is a key from 1 to 9, hence we need to decrease it by 1 to get an actual index in the list.

        foreach (Transform t in weaponHolder)
        {
            Destroy(t.gameObject);
        }

        weaponManager.ChangeIndex(index);

        var weapon = Instantiate(weaponManager.weapons[index]);
        currentWeapon = weapon.GetComponent<Weapon>();
        weapon.transform.SetParent(weaponHolder, false);
    }

    /// <summary>
    /// Used to take the next weapon.
    /// </summary>
    public void IncrementWeaponIndex()
        => weaponManager.ChangeIndex(weaponManager.CurrentIndex + 1 % weaponManager.weapons.Count);

    /// <summary>
    /// Used to take the previous weapon.
    /// </summary>
    public void DecrementWeaponIndex()
    => weaponManager.ChangeIndex(weaponManager.CurrentIndex - 1 % weaponManager.weapons.Count);
}
