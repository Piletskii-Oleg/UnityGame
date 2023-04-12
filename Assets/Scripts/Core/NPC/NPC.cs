using UnityEngine;

namespace Core.NPC
{
    /// <summary>
    /// A class that represents an NPC.
    /// </summary>
    public abstract class NPC : MonoBehaviour
    {
        /// <summary>
        /// Starts conversation with the NPC.
        /// </summary>
        public abstract void StartConversation();
    }
}