using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the weapons on the player.
/// </summary>
[CreateAssetMenu]
public class WeaponManager : ScriptableObject
{
    [field: SerializeField] public List<GameObject> Weapons { get; private set; }

    [SerializeField] private UnityEvent onChangeWeapon;

    /// <summary>
    /// Index of the weapon currently held by the player.
    /// </summary>
    public int CurrentIndex { get; private set; }

    /// <summary>
    /// Used to change the current index of the <see cref="weapons"/>.
    /// </summary>
    /// <param name="index">Index of the weapon in the list.</param>
    public void ChangeIndex(int index)
    {
        if (index < Weapons.Count && index >= 0)
        {
            CurrentIndex = index;

            onChangeWeapon.Invoke();
        }
    }
}
