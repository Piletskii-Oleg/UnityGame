using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Gun : Weapon
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;

    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private UnityEvent onReload;

    private float timeSinceLastShot;

    private WaitForSeconds reloadWait;

    public override void Shoot()
    {
        if (gunData.currentAmmo > 0 && CanShoot())
        {
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                var damageable = hitInfo.transform.GetComponent<Damageable>();
                if (damageable)
                {
                    damageable.InflictDamage(gunData.damage);
                }
            }

            gunData.currentAmmo--;
            timeSinceLastShot = 0;
        }

        onShoot.Invoke();
    }

    public override void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return reloadWait;

        gunData.currentAmmo = gunData.ammoCapacity;

        gunData.reloading = false;

        onReload.Invoke();
    }

    private void OnDisable()
        => gunData.reloading = false;

    private void Start()
    {
        timeSinceLastShot = 0f;
        gunData.currentAmmo = gunData.ammoCapacity;

        reloadWait = new WaitForSeconds(gunData.reloadTime);
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private bool CanShoot()
        => !gunData.reloading && timeSinceLastShot > (1f / gunData.fireRate);
}
