using System;
using UnityEngine;
using Weapons.ScriptableObjects;

namespace Weapons
{
    [Serializable]
    public class GunItem
    {
        [field:SerializeField] public GunData Data { get; private set; }
        
        [field:SerializeField] public bool IsObtained { get; private set; }
        
        public bool IsReloading { get; private set; }

        public GunItem(GunData data)
        {
            Data = data;
        }

        public void ObtainItem()
            => IsObtained = true;
    }
}