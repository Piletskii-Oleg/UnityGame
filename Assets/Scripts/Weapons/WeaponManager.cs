using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    private int currentIndex;
    private Weapon currentWeapon;

    private void Start()
    {
        currentIndex = 0;
        currentWeapon = weapons[currentIndex].GetComponent<Weapon>();
    }

    public void ChangeWeapon()
    {
        currentIndex = (currentIndex + 1) % weapons.Count;
        currentWeapon = weapons[currentIndex].GetComponent<Weapon>();
    }

    public void Shoot() => currentWeapon.Shoot();

    public void StartReload() => currentWeapon.StartReload();
}
