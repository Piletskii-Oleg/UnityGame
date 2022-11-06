using System.Collections;
using UnityEngine;

// put in WeaponManager.cs

/// <summary>
/// Processes zoom input.
/// </summary>
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

    /// <summary>
    /// Zooms in or out with the weapon held by the player.
    /// </summary>
    /// <param name="zoomIn">True if the player zooms in and false otherwise.</param>
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
