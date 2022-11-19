using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Guns
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
