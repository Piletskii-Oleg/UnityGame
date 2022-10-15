using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;

    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
    }

    public void Shoot() => weapons[currentIndex].Shoot();

    public void Reload() => weapons[currentIndex].Reload();
}
