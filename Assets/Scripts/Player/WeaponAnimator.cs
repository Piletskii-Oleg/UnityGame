using System.Linq;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private Animator animator;

    private AnimationClip zoomClip;
    private AnimationClip zoomOutClip;
    [SerializeField] private float zoomTimeMultiplier;

    public float ZoomAnimationTime => zoomClip.length * zoomTimeMultiplier;

    private void Start()
    {
        animator = GetComponent<Animator>();
        zoomClip = animator.runtimeAnimatorController.animationClips.First(clip => clip.name == "WeaponZoom");
        zoomOutClip = animator.runtimeAnimatorController.animationClips.First(clip => clip.name == "WeaponZoomOut");
    }

    public void Zoom(bool zoomIn)
    {
        animator.SetBool("ZoomIn", zoomIn);
    }
}
