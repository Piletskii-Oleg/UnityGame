using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    [SerializeField] private UnityEvent onChangeWeapon;

    public int CurrentIndex { get; private set; }
    private Weapon currentWeapon;

    private void Awake()
    {
        CurrentIndex = 0;
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();
    }

    public void ChangeWeapon(int index)
    {
        index--;
        if (index < weapons.Count && index >= 0)
        {
            weapons[CurrentIndex].SetActive(false);
            weapons[index].SetActive(true);
            CurrentIndex = index;
            currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();

            onChangeWeapon.Invoke();
        }
    }

    public void IncrementWeaponIndex()
    {
        CurrentIndex = (CurrentIndex + 1) % weapons.Count;
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();

        onChangeWeapon.Invoke();
    }

    public void DecrementWeaponIndex()
    {
        CurrentIndex = (CurrentIndex - 1) % weapons.Count;
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();

        onChangeWeapon.Invoke();
    }

    public void Shoot()
        => currentWeapon.Shoot();

    public void StartReload()
        => currentWeapon.StartReload();
}
