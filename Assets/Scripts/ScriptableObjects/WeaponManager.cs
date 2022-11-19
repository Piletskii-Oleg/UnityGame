using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the weapons on the player.
/// </summary>
[CreateAssetMenu(fileName = "Weapon Manager", menuName = "Managers/Weapon Manager")]
public class WeaponManager : ScriptableObject
{
    [SerializeField] private WeaponListData weaponsData;

    [SerializeField] private List<GameObject> weapons;

    [SerializeField] private UnityEvent onChangeWeapon;

    /// <summary>
    /// Index of the weapon currently held by the player.
    /// </summary>
    public int CurrentIndex { get; private set; }

    public GameObject CurrentWeaponPrefab => weapons[CurrentIndex];

    public GunData CurrentGunData => weaponsData.weapons[CurrentIndex];

    public int WeaponCount => weapons.Count;

    /// <summary>
    /// Used to change the current index of the <see cref="weapons"/>.
    /// </summary>
    /// <param name="index">Index of the weapon in the list.</param>
    public bool ChangeIndex(int index)
    {
        if (index < weapons.Count && index >= 0)
        {
            CurrentIndex = index;

            onChangeWeapon.Invoke();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Used to change the current index of the <see cref="weapons"/>.
    /// Does nothing if <paramref name="name"/> is not found in the list.
    /// </summary>
    /// <param name="name">Name of the weapon in the list.</param>
    public bool ChangeTo(string name) // completely breaks if order is not same as in weaponsData.weapons
    {
        var newIndex = weaponsData.weapons.FindIndex(gameObject => gameObject.name == name);
        if (newIndex == -1)
        {
            return false;
        }

        if (!weapons.Contains(weaponsData.weapons[newIndex].gunPrefab))
        {
            weapons.Add(weaponsData.weapons[newIndex].gunPrefab);
        }

        CurrentIndex = newIndex;
        onChangeWeapon.Invoke();
        return true;
    }
}
