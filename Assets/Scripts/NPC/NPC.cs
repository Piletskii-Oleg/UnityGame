using System.Collections;
using NPC.Dialogue;
using Player.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace NPC
{
    /// <summary>
    /// A class that represents an NPC.
    /// </summary>
    public abstract class NPC : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] protected PlayerScriptableObject playerScriptableObject;
        [SerializeField] protected DialogueManager dialogueManager;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<DialogueManager> startConversationEvent;
                
        private Coroutine turnHeadCoroutine;
        
        protected Animator animator;

        /// <summary>
        /// Starts conversation with the NPC.
        /// </summary>
        public virtual void StartConversation()
        {
            startConversationEvent.Invoke(dialogueManager);
        }
        
        protected void TurnTransform(Transform transformToTurn, Vector3 position)
        {
            if (turnHeadCoroutine != null)
            {
                StopCoroutine(turnHeadCoroutine);
            }

            turnHeadCoroutine =
                StartCoroutine(TurnHeadToPosition(transformToTurn, position, 4f));
        }

        private IEnumerator TurnHeadToPosition(Transform transformToTurn, Vector3 position, float smoothing)
        {
            var initialPosition = transformToTurn.position;
            
            var direction = position - initialPosition;
            var forwardVector = transformToTurn.rotation * Vector3.forward + initialPosition - initialPosition;
            
            var toRotation = Quaternion.FromToRotation(- forwardVector, direction);
            while (Vector3.Angle(forwardVector, direction) > 7f)
            {
                transformToTurn.rotation = 
                    Quaternion.RotateTowards(transformToTurn.rotation, toRotation, smoothing);

                yield return null;
            }
        }
        
        /// <summary>
        /// Triggers an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        protected void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);
    }
}