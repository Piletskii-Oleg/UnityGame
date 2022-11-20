using System.Collections.Generic;
using UnityEngine;

namespace Weapons.ScriptableObjects
{
    /// <summary>
    /// Data for all the weapons held by an entity in the world.
    /// </summary>
    [CreateAssetMenu]
    public class WeaponListData : ScriptableObject
    {
        public List<GunData> weapons;
    }
}
