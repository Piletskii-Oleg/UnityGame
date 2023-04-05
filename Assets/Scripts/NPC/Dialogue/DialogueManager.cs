using UnityEngine;

namespace NPC.Dialogue
{
    [CreateAssetMenu(menuName = "Actor/Dialogue/Dialogue Manager", order = 0)]
    public class DialogueManager : ScriptableObject
    {
        [field: SerializeField] public string NpcName { get; private set; }
        [SerializeField] private DialogueObject startingDialogue;
        [SerializeField] private DialogueObject currentDialogue;

        public DialogueObject CurrentDialogue
        {
            get
            {
                if (!currentDialogue || currentDialogue.Name == "End")
                {
                    return startingDialogue;
                }
                else
                {
                    return currentDialogue;
                }
            }
            private set => currentDialogue = value;
        }

        public void ChangeCurrentDialogue(string dialogueName)
            => CurrentDialogue = CurrentDialogue.GetDialogue(dialogueName);
    }
}