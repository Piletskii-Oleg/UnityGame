using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;

    private float timeSinceLastShot;

    private WaitForSeconds reloadWait;
    private WaitForSeconds rapidFireWait;

    public override bool IsAutomatic => gunData.canAutoShoot;

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
    }

    public override void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    public override IEnumerator RapidFire()
    {
        if (CanShoot())
        {
            while (gunData.currentAmmo > 0)
            {
                Shoot();
                yield return rapidFireWait;
            }
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return reloadWait;

        gunData.currentAmmo = gunData.ammoCapacity;

        gunData.reloading = false;
    }

    private void OnDisable()
        => gunData.reloading = false;

    private void Start()
    {
        timeSinceLastShot = 0f;
        gunData.currentAmmo = gunData.ammoCapacity;

        reloadWait = new WaitForSeconds(gunData.reloadTime);
        rapidFireWait = new WaitForSeconds(1f / gunData.fireRate);
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private bool CanShoot()
        => !gunData.reloading && timeSinceLastShot > (1f / gunData.fireRate);
}
