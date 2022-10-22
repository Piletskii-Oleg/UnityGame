using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Shoot();

    public abstract void StartReload();

    public abstract bool IsAutomatic { get; }
}
