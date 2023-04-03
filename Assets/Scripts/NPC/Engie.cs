using System.Collections;
using Player.ScriptableObjects;
using UnityEngine;

namespace NPC
{
    public class Engie : NPC
    {
        private static readonly int waveHand = Animator.StringToHash("WaveHand");
        
        private Animator animator;
        private Coroutine turnHeadCoroutine;

        [Header("Player Data")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Robot elements")]
        [SerializeField] private Transform headTransform;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public override void StartConversation()
        {
            TurnHead();
            WaveHand();
        }

        private void TurnHead()
        {
            if (turnHeadCoroutine != null)
            {
                StopCoroutine(turnHeadCoroutine);
            }

            turnHeadCoroutine = StartCoroutine(TurnHeadToPlayer(4f));
        }

        private IEnumerator TurnHeadToPlayer(float smoothing)
        { 
            var playerPosition = playerScriptableObject.GetActualCameraPosition();
            var headPosition = headTransform.position;
            var direction = headPosition - playerPosition;

            var waitForSeconds = new WaitForSeconds(0.01f);
            
            while (Vector3.Angle(-headTransform.position + headTransform.forward, headTransform.position - playerPosition) > 7f)
            {
                var toRotation = Quaternion.FromToRotation(headTransform.position, direction);
                headTransform.rotation = Quaternion.Lerp(headTransform.rotation, toRotation, smoothing * Time.deltaTime);

                yield return waitForSeconds;
            }
        }

        /// <summary>
        /// Triggers an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        public void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);

        private void WaveHand()
        {
            TriggerAnimation(waveHand);
        }
    }
}