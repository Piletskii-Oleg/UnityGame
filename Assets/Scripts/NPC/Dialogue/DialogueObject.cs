using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NPC.Dialogue
{
    [CreateAssetMenu(menuName = "Actor/Dialogue/DialogueObject", order = 0)]
    public class DialogueObject : ScriptableObject
    {
        [SerializeField] private string dialogueOptionName;
        
        [TextArea, SerializeField] private string dialogueText;

        [SerializeField] private List<DialogueObject> nextDialogues;

        [SerializeField] private UnityEvent dialogueEvent;

        public string Text => dialogueText;
        
        public string Name => dialogueOptionName;

        public IReadOnlyList<DialogueObject> NextDialogues => nextDialogues;

        public DialogueObject GetDialogue(int index)
            => nextDialogues[index];

        public DialogueObject GetDialogue(string dialogueName)
            => nextDialogues.Find(dialogue => dialogue.dialogueOptionName == dialogueName);
    }
}