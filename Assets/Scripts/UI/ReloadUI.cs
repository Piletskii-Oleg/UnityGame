using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Weapons;
using Weapons.ScriptableObjects;

namespace UI
{
    /// <summary>
    /// UI for the reloading of a <see cref="IWeapon"/>.
    /// </summary>
    public class ReloadUI : MonoBehaviour
    {
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private Slider slider;
        
        private WaitForFixedUpdate waitForFixedUpdate;

        private void Start()
        {
            waitForFixedUpdate = new WaitForFixedUpdate();
        }

        /// <summary>
        /// Called when the gun is reloaded.
        /// </summary>
        public void OnReload()
        {
            StartCoroutine(FillReloadBar());
        }

        /// <summary>
        /// Called when the current gun is changed.
        /// </summary>
        public void OnChangeWeapon()
        {
            slider.value = 1;
        }

        private IEnumerator FillReloadBar()
        {
            slider.gameObject.SetActive(true);
            slider.value = 0;

            while (slider.value < 1)
            {
                slider.value += Time.deltaTime / weaponManager.CurrentGunData.reloadTime;
                yield return waitForFixedUpdate;
            }

            slider.gameObject.SetActive(false);
        }
    }
}