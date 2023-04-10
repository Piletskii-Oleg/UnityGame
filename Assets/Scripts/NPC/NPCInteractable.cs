using Interactable;
using UnityEngine;

namespace NPC
{
    public class NPCInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptMessage;
        [SerializeField] private NPC npc;
        
        public void Interact()
            => npc.StartConversation();

        public string GetPromptMessage()
            => promptMessage;
    }
}