using System.Collections;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    private WeaponAnimator weaponAnimator;

    [SerializeField] private float zoomInFOV;

    private float baseFOV;
    private Camera mainCamera;

    private Coroutine zoomAnimationCoroutine;

    private void Start()
    {
        mainCamera = Camera.main;
        baseFOV = mainCamera.fieldOfView;
        weaponAnimator = GetComponentInChildren<WeaponAnimator>();
    }

    public void Zoom(bool zoomIn)
    {
        if (zoomAnimationCoroutine != null)
        {
            StopCoroutine(zoomAnimationCoroutine);
        }

        zoomAnimationCoroutine = StartCoroutine(ZoomAnimation(zoomIn));
    }

    private IEnumerator ZoomAnimation(bool zoomIn)
    {
        weaponAnimator.Zoom(zoomIn);

        float animationTime = weaponAnimator.ZoomAnimationTime;
        float elapsed = 0f;

        float startFOV = mainCamera.fieldOfView;
        float endFOV = zoomIn ? zoomInFOV : baseFOV;

        while (elapsed < animationTime)
        {
            var currentFOV = Mathf.Lerp(startFOV, endFOV, elapsed / animationTime);
            mainCamera.fieldOfView = currentFOV;

            elapsed += Time.fixedDeltaTime;
            yield return null;
        }
    }
}
