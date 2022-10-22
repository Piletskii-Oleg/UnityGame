using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract bool IsAutomatic { get; }

    public abstract void Shoot();

    public abstract void StartReload();

    public abstract IEnumerator RapidFire();
}
