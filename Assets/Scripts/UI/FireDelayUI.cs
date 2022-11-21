using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Weapons.ScriptableObjects;

namespace UI
{
    public class FireDelayUI : MonoBehaviour
    {
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private Slider slider;

        private WaitForFixedUpdate waitForFixedUpdate;

        private void Start()
            => waitForFixedUpdate = new WaitForFixedUpdate();

        public void UpdateBar()
            => slider.gameObject.SetActive(!weaponManager.CurrentGunData.canAutoShoot);

        public void FillBar()
        {
            slider.value = 0;
            StartCoroutine(FillBarByDeltaTime());
        }

        private IEnumerator FillBarByDeltaTime()
        {
            while (slider.value < 1)
            {
                slider.value += Time.fixedDeltaTime * weaponManager.CurrentGunData.fireRate / 60f;
                yield return waitForFixedUpdate;
            }
        }
    }
}