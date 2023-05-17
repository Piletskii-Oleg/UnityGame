using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using Weapons;

namespace DataPersistence.DataFiles
{
    /// <summary>
    /// C# class that contains all data that should be saved or loaded.
    /// </summary>
    [Serializable]
    public class GameData
    {
        public List<InventoryItem> storedItems;
        
        public List<GunItem> storedWeapons;
        public int currentWeaponIndex;

        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Vector3 playerVelocity;

        public Quaternion cameraRotation;

        public float health;
        
        /// <summary>
        /// Initializes new instance of <see cref="GameData"/> class.
        /// </summary>
        public GameData()
        {
            storedItems = new List<InventoryItem>();
            storedWeapons = new List<GunItem>();
        }
    }
}