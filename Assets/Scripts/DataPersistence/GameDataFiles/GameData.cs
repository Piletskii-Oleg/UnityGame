using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using Weapons;

namespace DataPersistence.GameDataFiles
{
    [Serializable]
    public class GameData
    {
        public List<InventoryItem> storedItems;
        
        public List<GunItem> storedWeapons;
        public int currentWeaponIndex;

        public Vector3 playerPosition;
        public Quaternion playerRotation;

        public Quaternion cameraRotation;
        
        public GameData()
        {
            storedItems = new();
            storedWeapons = new();
        }
    }
}