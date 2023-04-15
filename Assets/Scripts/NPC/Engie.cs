using UnityEngine;

namespace NPC
{
    public class Engie : NPC
    {
        private static readonly int waveHand = Animator.StringToHash("WaveHand");

        [Tooltip("Transform at which Engie looks after finishing conversation with the player")]
        [SerializeField] private Transform lookAt;

        [Header("Robot elements")]
        [SerializeField] private Transform headTransform;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        
        public override void StartConversation()
        {
            base.StartConversation();
            
            TurnTransform(headTransform, playerScriptableObject.GetActualPlayerPosition());
            WaveHand();
        }

        public void OnEndConversation()
            => TurnTransform(headTransform, lookAt.position);

        private void WaveHand()
            => TriggerAnimation(waveHand);
    }
}