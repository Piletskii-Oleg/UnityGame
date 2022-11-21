using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Weapons.ScriptableObjects;

namespace UI
{
    public class ReloadUI : MonoBehaviour
    {
        [SerializeField] private WeaponManager weaponManager;
        [SerializeField] private Slider slider;
        
        private WaitForFixedUpdate waitForFixedUpdate;

        private void Start()
            => waitForFixedUpdate = new WaitForFixedUpdate();

        public void OnReload()
            => StartCoroutine(FillReloadBar());

        public void OnChangeWeapon()
            => slider.value = 1;

        private IEnumerator FillReloadBar()
        {
            slider.gameObject.SetActive(true);
            slider.value = 0;
            
            while (slider.value < 1)
            {
                slider.value += Time.fixedDeltaTime / weaponManager.CurrentGunData.reloadTime;
                yield return waitForFixedUpdate;
            }

            slider.gameObject.SetActive(false);
        }
    }
}