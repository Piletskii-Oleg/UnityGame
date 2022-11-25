using System;
using UnityEngine;
using Weapons.ScriptableObjects;

namespace Weapons
{
    /// <summary>
    /// Wrap around <see cref="GunData"/> that stores runtime-specific data.
    /// </summary>
    [Serializable]
    public class GunItem
    {
        /// <summary>
        /// Stores <see cref="GunData"/> of a gun.
        /// </summary>
        [field:SerializeField] public GunData Data { get; private set; }
        
        /// <summary>
        /// Gets value indicating whether player has this weapon or not.
        /// </summary>
        [field:SerializeField] public bool IsObtained { get; private set; }
        
        public bool IsReloading { get; private set; } // unused

        /// <summary>
        /// Initializes new instance of an <see cref="GunItem"/> class.
        /// </summary>
        /// <param name="data"><see cref="GunData"/> to wrap around.</param>
        public GunItem(GunData data)
        {
            Data = data;
        }

        /// <summary>
        /// Marks the weapon as obtained.
        /// </summary>
        public void ObtainItem()
            => IsObtained = true;
    }
}