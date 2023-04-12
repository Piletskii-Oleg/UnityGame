using Core.Weapons.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Functionality.UI
{
    /// <summary>
    /// UI that shows player's ammo.
    /// </summary>
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private WeaponManager weaponManager;

        /// <summary>
        /// Updates ammo counter on the UI.
        /// </summary>
        public void UpdateAmmo()
            => text.text = $"{weaponManager.CurrentGunData.currentAmmo} / {weaponManager.CurrentGunData.ammoCapacity}";

        /// <summary>
        /// Updates ammo counter on the UI when the weapon is changed.
        /// </summary>
        public void UpdateWeapon()
            => UpdateAmmo();
    }
}
