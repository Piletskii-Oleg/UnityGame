using UnityEngine;

namespace NPC
{
    public class Engie : Robot
    {
        private static readonly int waveHand = Animator.StringToHash("WaveHand");

        public override void StartConversation()
        {
            base.StartConversation();
            
            WaveHand();
        }
        
        private void WaveHand()
            => TriggerAnimation(waveHand);
    }
}