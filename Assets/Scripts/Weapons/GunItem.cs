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
        [field:SerializeField] public string Name { get; private set; }
        
        /// <summary>
        /// Stores <see cref="GunData"/> of a gun.
        /// </summary>
        [field:SerializeField] public GunData Data { get; private set; }

        /// <summary>
        /// Gets value indicating whether player has this weapon or not.
        /// </summary>
        [field:SerializeField] public bool IsObtained { get; private set; }

        /// <summary>
        /// Gets value indicating whether a gun has shot and appropriate time between shots has not yet passed.
        /// </summary>
        [field:SerializeField] public bool HasShot { get; set; }

        public int currentTotalAmmo;

        public int currentAmmo;

        /// <summary>
        /// Initializes new instance of an <see cref="GunItem"/> class.
        /// </summary>
        /// <param name="data"><see cref="GunData"/> to wrap around.</param>
        public GunItem(GunData data)
        {
            Data = data;
            Name = data.name;
        }

        public void SetData(GunData data)
        {
            Data = data;
        }

        /// <summary>
        /// Marks the weapon as obtained.
        /// </summary>
        public void ObtainItem()
            => IsObtained = true;

        public bool TryAddAmmo(int ammoCount)
        {
            if (Data.currentTotalAmmo < Data.totalAmmo)
            {
                Data.currentTotalAmmo += ammoCount;
                if (Data.currentTotalAmmo > Data.totalAmmo)
                {
                    Data.currentTotalAmmo = Data.totalAmmo;
                }
                
                return true;
            }

            return false;
        }

        public void SaveData()
        {
            currentTotalAmmo = Data.currentTotalAmmo;
            currentAmmo = Data.currentAmmo;
        }
        
        public void LoadData()
        {
            Data.currentTotalAmmo = currentTotalAmmo;
            Data.currentAmmo = currentAmmo;
        }
    }
}