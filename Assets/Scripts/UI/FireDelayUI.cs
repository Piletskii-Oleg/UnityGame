using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Weapons.ScriptableObjects;

namespace UI
{
    /// <summary>
    /// UI that shows fire delay using a slider.
    /// </summary>
    public class FireDelayUI : MonoBehaviour
    {
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private Slider slider;

        private WaitForFixedUpdate waitForFixedUpdate;

        private void Start()
            => waitForFixedUpdate = new WaitForFixedUpdate();

        /// <summary>
        /// Called when another gun is taken.
        /// </summary>
        public void UpdateBar()
            => slider.gameObject.SetActive(!weaponManager.CurrentGunData.canAutoShoot);

        /// <summary>
        /// Slowly fills the bar after a gun shot.
        /// </summary>
        public void FillBar()
        {
            slider.value = 0;
            StartCoroutine(FillBarByTime());
        }

        private IEnumerator FillBarByTime()
        {
            while (slider.value < 1)
            {
                slider.value += Time.deltaTime * weaponManager.CurrentGunData.fireRate / 60f;
                yield return waitForFixedUpdate;
            }
        }
    }
}