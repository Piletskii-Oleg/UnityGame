using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NPC.Dialogue
{
    /// <summary>
    /// An abstraction over a dialogue option.
    /// </summary>
    [CreateAssetMenu(menuName = "Actor/Dialogue/DialogueObject")]
    public class DialogueObject : ScriptableObject
    {
        [Tooltip("Option to choose this dialogue object. It is shown to the player in the dialogue box.")]
        [SerializeField] private string dialogueOptionName;
        
        [Tooltip("Text that pops up when player chooses this dialogue object.")]
        [TextArea(7, 10), SerializeField] private string dialogueText;

        [Tooltip("Dialogue objects that this object leads to.")]
        [SerializeField] private List<DialogueObject> nextDialogues;

        [SerializeField] private UnityEvent dialogueEvent;

        /// <summary>
        /// Text that pops up when player chooses this dialogue object.
        /// </summary>
        public string Text => dialogueText;
        
        /// <summary>
        /// Option to choose this dialogue object. It is shown to the player in the dialogue box.
        /// </summary>
        public string Name => dialogueOptionName;

        /// <summary>
        /// Dialogue objects that this object leads to.
        /// </summary>
        public IReadOnlyList<DialogueObject> NextDialogues => nextDialogues;

        /// <summary>
        /// Gets dialogue based on its name (see <see cref="Name"/>).
        /// </summary>
        /// <param name="dialogueName">Option to choose this dialogue object.</param>
        /// <returns>Next dialogue object. Null if it cannot be found.</returns>
        public DialogueObject GetDialogue(string dialogueName)
            => nextDialogues.Find(dialogue => dialogue.dialogueOptionName == dialogueName);

        /// <summary>
        /// Gets dialogue based on its name AND invokes the <see cref="dialogueEvent"/>.
        /// </summary>
        /// <param name="dialogueName">Option to choose this dialogue object.
        /// Can be accessed via <see cref="Name"/>.</param>
        /// <returns>Next dialogue object. Null if it cannot be found.</returns>
        public DialogueObject ChangeDialogue(string dialogueName)
        {
            var nextDialogue = GetDialogue(dialogueName);
            if (nextDialogue != null)
            {
                dialogueEvent.Invoke();
            }

            return nextDialogue;
        }
    }
}