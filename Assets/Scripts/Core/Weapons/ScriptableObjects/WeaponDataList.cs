using System.Collections.Generic;
using UnityEngine;

namespace Core.Weapons.ScriptableObjects
{
    /// <summary>
    /// Data for all the weapons held by an entity in the world.
    /// </summary>
    [CreateAssetMenu(menuName = "Data List/Weapon Data List")]
    public class WeaponDataList : ScriptableObject
    {
        public List<GunData> weapons;
    }
}
