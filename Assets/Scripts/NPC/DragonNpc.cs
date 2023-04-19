using UnityEngine;

namespace NPC
{
    public class DragonNpc : NPC
    {
        private static readonly int startTalk = Animator.StringToHash("StartTalk");
        private static readonly int endTalk = Animator.StringToHash("EndTalk");
        
        [SerializeField] private Transform headTransform;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
    }
}