using System.Collections;
using DG.Tweening;
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

        protected Animator animator;

        /// <summary>
        /// Starts conversation with the NPC.
        /// </summary>
        public virtual void StartConversation()
        {
            startConversationEvent.Invoke(dialogueManager);
        }

        /// <summary>
        /// Triggers an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        protected void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);
    }
}