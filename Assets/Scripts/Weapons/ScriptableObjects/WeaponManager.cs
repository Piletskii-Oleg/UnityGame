using System.Collections.Generic;
using DataPersistence;
using DataPersistence.DataFiles;
using UnityEngine;
using UnityEngine.Events;

namespace Weapons.ScriptableObjects
{
    /// <summary>
    /// Manages the weapons on the player.
    /// </summary>
    [CreateAssetMenu(fileName = "Weapon Manager", menuName = "Managers/Weapon Manager")]
    public class WeaponManager : DataManager<GameData>
    {
        [SerializeField] private List<GunItem> weapons;

        [SerializeField] private UnityEvent onChangeWeapon;

        [SerializeField] private WeaponDataList allWeaponsData;
        
        /// <summary>
        /// Gets index of the weapon currently held by the player.
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <summary>
        /// Gets <see cref="GunItem"/> that contains the <see cref="GunData"/> of the weapon currently held by the player.
        /// </summary>
        public GunItem CurrentGunItem => weapons[CurrentIndex];
        
        /// <summary>
        /// Gets <see cref="GunData"/> of the weapon currently held by the player.
        /// </summary>
        public GunData CurrentGunData => weapons[CurrentIndex].Data;
        
        /// <summary>
        /// Gets <see cref="GameObject"/> prefab of the weapon currently held by the player.
        /// </summary>
        public GameObject CurrentWeaponPrefab => weapons[CurrentIndex].Data.gunPrefab;

        /// <summary>
        /// Gets amount of weapons currently held by the player.
        /// </summary>
        public int WeaponCount => weapons.Count;

        public GunItem GetWeaponItem(string weaponName)
            => weapons.Find(weapon => weapon.Name == weaponName);

        /// <summary>
        /// Used to change the current index of the <see cref="weapons"/>.
        /// </summary>
        /// <param name="index">Index of the weapon in the list.</param>
        public bool ChangeIndex(int index)
        {
            if (index < weapons.Count && index >= 0 && weapons[index].IsObtained)
            {
                CurrentIndex = index;

                onChangeWeapon.Invoke();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Used to change the current index of the <see cref="weapons"/>.
        /// Does nothing if <paramref name="weaponName"/> is not found on the list.
        /// </summary>
        /// <param name="weaponName">Name of the weapon in the list.</param>
        public bool ChangeTo(string weaponName) // makes weapon selectable, maybe fix?
        {
            int newIndex = weapons.FindIndex(item => item.Data.showName == weaponName);
            if (newIndex == -1)
            {
                return false;
            }

            if (!weapons[newIndex].IsObtained)
            {
                weapons[newIndex].ObtainItem();
            }

            CurrentIndex = newIndex;
            onChangeWeapon.Invoke();
            return true;
        }

        public override void SaveData(GameData data)
        {
            foreach (var item in weapons)
            {
                item.SaveData();
            }
            
            data.currentWeaponIndex = CurrentIndex;
            data.storedWeapons = weapons;
        }

        public override void LoadData(GameData data)
        {
            weapons = data.storedWeapons;
            foreach (var item in weapons)
            {
                var foundData = allWeaponsData.weapons.Find(value => value.showName == item.Name);
                item.SetData(foundData);
                
                item.LoadData();
            }
            
            CurrentIndex = data.currentWeaponIndex;

            foreach (var item in weapons)
            {
                item.HasShot = false;
            }
        }

        public bool TryPickUpAmmo(GunData gunData, int ammoCount) =>
            weapons
                .Find(x => x.Data == gunData)
                .TryAddAmmo(ammoCount);
    }
}
