using UnityEngine;

namespace NPC.Dialogue
{
    /// <summary>
    /// Manages the dialogue flow of some NPC.
    /// </summary>
    [CreateAssetMenu(menuName = "Actor/Dialogue/Dialogue Manager", order = 0)]
    public class DialogueManager : ScriptableObject
    {
        [Tooltip("Name of the NPC that this dialogue manager belongs to.")]
        [field: SerializeField] public string NpcName { get; private set; }
        
        [Tooltip("Dialogue object that is selected if the dialogue had never begun.")]
        [SerializeField] private DialogueObject firstDialogue;

        [Tooltip("Dialogue object that is selected if the dialogue had ended.")]
        [SerializeField] private DialogueObject startingDialogue;
        
        [Tooltip("Dialogue object that is currently shown to the player.")]
        [SerializeField] private DialogueObject currentDialogue;

        /// <summary>
        /// Gets current dialogue.
        /// </summary>
        public DialogueObject CurrentDialogue
        {
            get
            {
                if (!currentDialogue)
                {
                    return firstDialogue;
                }
                
                return currentDialogue.Name == "End" ? startingDialogue : currentDialogue;
            }
            private set => currentDialogue = value;
        }

        /// <summary>
        /// Changes current dialogue object to some next one, based on <paramref name="dialogueName"/>.
        /// </summary>
        /// <param name="dialogueName">Name of the next dialogue.
        /// Can be accessed via <see cref="DialogueObject.Name"/>.</param>
        public void ChangeCurrentDialogueObject(string dialogueName)
            => CurrentDialogue = CurrentDialogue.ChangeDialogue(dialogueName);

        public void SetCurrentDialogueObject(DialogueObject dialogueObject)
            => CurrentDialogue = dialogueObject;
    }
}