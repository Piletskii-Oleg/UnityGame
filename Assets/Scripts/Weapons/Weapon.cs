using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private GunData gunData;

    public abstract void Shoot();

    public abstract void Reload();
}
