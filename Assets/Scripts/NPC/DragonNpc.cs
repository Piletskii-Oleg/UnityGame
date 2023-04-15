using System;
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

        public override void StartConversation()
        {
            base.StartConversation();
            
            TriggerAnimation(startTalk);
            
            TurnTransform(transform, playerScriptableObject.GetActualPlayerPosition());
        }

        public void StopConversation()
        {
            TriggerAnimation(endTalk);
        }
    }
}