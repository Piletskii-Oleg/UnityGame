using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the weapons on the player.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    [SerializeField] private UnityEvent onChangeWeapon;

    /// <summary>
    /// Index of the weapon currently held by the player.
    /// </summary>
    public int CurrentIndex { get; private set; }
    private Weapon currentWeapon;

    private void Awake()
    {
        CurrentIndex = weapons.FindIndex(weapon => weapon.activeInHierarchy);
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();
        onChangeWeapon.Invoke();
    }

    /// <summary>
    /// Used to change the currently held weapon.
    /// </summary>
    /// <param name="index">Index of the weapon in the list.</param>
    public void ChangeWeapon(int index)
    {
        index--; // index parameter is a key from 1 to 9, hence we need to decrease it by 1 to get an actual index in the list. 
        if (index < weapons.Count && index >= 0)
        {
            weapons[CurrentIndex].SetActive(false);
            weapons[index].SetActive(true);
            CurrentIndex = index;
            currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();

            onChangeWeapon.Invoke();
        }
    }

    /// <summary>
    /// Used to take the next weapon.
    /// </summary>
    public void IncrementWeaponIndex()
    {
        CurrentIndex = (CurrentIndex + 1) % weapons.Count;
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();

        onChangeWeapon.Invoke();
    }

    /// <summary>
    /// Used to take the previous weapon.
    /// </summary>
    public void DecrementWeaponIndex()
    {
        CurrentIndex = (CurrentIndex - 1) % weapons.Count;
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();

        onChangeWeapon.Invoke();
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
}
