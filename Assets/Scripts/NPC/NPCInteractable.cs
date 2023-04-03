using Interactable;
using UnityEngine;

namespace NPC
{
    public class NPCInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private NPC npc;
        
        public void Interact()
        {
            npc.StartConversation();
        }

        public string GetPromptMessage() => "Press E to talk to NPC";
    }
}