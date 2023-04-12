using System.Collections;
using Core.Player.ScriptableObjects;
using Functionality.NPC.Dialogue;
using UnityEngine;
using UnityEngine.Events;

namespace Functionality.NPC
{
    public class Engie : Core.NPC.NPC
    {
        private static readonly int waveHand = Animator.StringToHash("WaveHand");
        
        private Animator animator;
        private Coroutine turnHeadCoroutine;

        [Header("Data")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [SerializeField] private DialogueManager dialogueManager;
        
        [Tooltip("Transform at which Engie looks after finishing conversation with the player")]
        [SerializeField] private Transform lookAt;

        [Header("Robot elements")]
        [SerializeField] private Transform headTransform;

        [Header("Events")]
        [SerializeField] private UnityEvent<DialogueManager> startConversationEvent;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        
        public override void StartConversation()
        {
            startConversationEvent.Invoke(dialogueManager);
            
            TurnHead(playerScriptableObject.GetActualPlayerPosition());
            WaveHand();
        }

        private void TurnHead(Vector3 position)
        {
            if (turnHeadCoroutine != null)
            {
                StopCoroutine(turnHeadCoroutine);
            }

            turnHeadCoroutine =
                StartCoroutine(TurnHeadToPosition(4f, position));
        }

        private IEnumerator TurnHeadToPosition(float smoothing, Vector3 position)
        {
            var headPosition = headTransform.position;
            var direction = headPosition - position;

            var waitForSeconds = new WaitForSeconds(0.01f);

            while (Vector3.Angle(-headTransform.position + headTransform.forward, headTransform.position - position) >
                   7f)
            {
                var toRotation = Quaternion.FromToRotation(headTransform.position, direction);
                headTransform.rotation = Quaternion.Lerp(headTransform.rotation,
                    toRotation, smoothing * Time.deltaTime);

                yield return waitForSeconds;
            }
        }

        /// <summary>
        /// Triggers an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        private void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);

        public void OnEndConversation()
            => TurnHead(lookAt.position);

        private void WaveHand()
            => TriggerAnimation(waveHand);
    }
}