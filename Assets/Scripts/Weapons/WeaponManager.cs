using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;

    public int CurrentIndex { get; private set; }
    private Weapon currentWeapon;

    private Coroutine fireCoroutine;

    private void Start()
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
        }
    }

    public void IncrementWeaponIndex()
    {
        CurrentIndex = (CurrentIndex + 1) % weapons.Count;
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();
    }

    public void DecrementWeaponIndex()
    {
        CurrentIndex = (CurrentIndex - 1) % weapons.Count;
        currentWeapon = weapons[CurrentIndex].GetComponent<Weapon>();
    }

    public void StartFiring()
    {
        if (IsCurrentWeaponAutomatic())
        {
            fireCoroutine = StartCoroutine(currentWeapon.RapidFire());
        }
        else
        {
            currentWeapon.Shoot();
        }
    }

    public void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }

    public void StartReload()
        => currentWeapon.StartReload();

    private bool IsCurrentWeaponAutomatic()
        => currentWeapon.IsAutomatic;
}
