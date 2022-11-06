using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class for all weapons.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// Method that is called when weapon shoots.
    /// </summary>
    public abstract void Shoot();

    /// <summary>
    /// Method called when weapon has to reload.
    /// </summary>
    public abstract void StartReload();
}
