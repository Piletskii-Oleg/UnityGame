using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    private int currentIndex;
    private Weapon currentWeapon;

    public bool IsCurrentWeaponAutomatic()
        => currentWeapon.IsAutomatic;

    private void Start()
    {
        currentIndex = 0;
        currentWeapon = weapons[currentIndex].GetComponent<Weapon>();
    }

    public void ChangeWeapon(int index)
    {
        index--;
        if (index < weapons.Count && index >= 0)
        {
            weapons[currentIndex].SetActive(false);
            weapons[index].SetActive(true);
            currentIndex = index;
            currentWeapon = weapons[currentIndex].GetComponent<Weapon>();
        }
    }
     
    public void IncrementWeaponIndex()
    {
        currentIndex = (currentIndex + 1) % weapons.Count;
        currentWeapon = weapons[currentIndex].GetComponent<Weapon>();
    }

    public void DecrementWeaponIndex()
    {
        currentIndex = (currentIndex - 1) % weapons.Count;
        currentWeapon = weapons[currentIndex].GetComponent<Weapon>();
    }

    public void Shoot() => currentWeapon.Shoot();

    public void StartReload() => currentWeapon.StartReload();
}
