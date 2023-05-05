using System;
using TMPro;
using UnityEngine;
using Weapons.ScriptableObjects;

namespace UI
{
    public class TotalAmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private WeaponManager weaponManager;
        
        /// <summary>
        /// Updates ammo counter on the UI.
        /// </summary>
        public void UpdateAmmo()
            => text.text = $"{weaponManager.CurrentGunData.currentTotalAmmo} / {weaponManager.CurrentGunData.totalAmmo}";

        private void Awake()
            => UpdateAmmo();
    }
}