using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        Debug.Log("Shooting!");
    }

    public override void Reload()
    {
        Debug.Log("Reloading!");
    }
}
