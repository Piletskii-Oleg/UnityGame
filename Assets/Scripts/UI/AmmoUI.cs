using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private List<GunData> gunDataList;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private WeaponManager weaponManager;

    private int currentIndex;

    private void Awake()
    {
        currentIndex = weaponManager.CurrentIndex;
    }

    public void UpdateAmmo()
    {
        text.text = gunDataList[currentIndex].currentAmmo + " / " + gunDataList[currentIndex].ammoCapacity;
    }

    public void UpdateWeapon()
    {
        currentIndex = weaponManager.CurrentIndex;
        UpdateAmmo();
    }
}
