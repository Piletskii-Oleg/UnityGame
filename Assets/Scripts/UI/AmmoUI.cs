using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// UI that shows player's ammo.
/// </summary>
public class AmmoUI : MonoBehaviour
{
    [SerializeField] private List<GunData> gunDataList;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private WeaponManager weaponManager;

    private int currentIndex;

    /// <summary>
    /// Updates ammo counter on the UI.
    /// </summary>
    public void UpdateAmmo()
    {
        text.text = gunDataList[currentIndex].currentAmmo + " / " + gunDataList[currentIndex].ammoCapacity;
    }

    /// <summary>
    /// Updates ammo counter on the UI when the weapon is changed.
    /// </summary>
    public void UpdateWeapon()
    {
        currentIndex = weaponManager.CurrentIndex;
        UpdateAmmo();
    }
}
