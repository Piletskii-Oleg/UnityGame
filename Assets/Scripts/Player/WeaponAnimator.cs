using System.Linq;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Class used to animate the weapons wielded by the player.
    /// </summary>
    public class WeaponAnimator : MonoBehaviour
    {
        private Animator animator;

        private AnimationClip zoomClip;
        [SerializeField] private float zoomTimeMultiplier;
        private static readonly int ZoomIn = Animator.StringToHash("ZoomIn");

        /// <summary>
        /// Gets time required for zoom animation to fully complete.
        /// </summary>
        public float ZoomAnimationTime => zoomClip.length * zoomTimeMultiplier;

        /// <summary>
        /// Zooms in or out with the weapon held by the player.
        /// </summary>
        /// <param name="zoomIn">True if the player zooms in and false otherwise.</param>
        public void Zoom(bool zoomIn)
        {
            animator.SetBool(ZoomIn, zoomIn);
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();

            zoomClip = animator.runtimeAnimatorController.animationClips.First(clip => clip.name.Contains("Zoom"));
        }

        private void OnDestroy()
        {
            animator = null;
        }
    }
}