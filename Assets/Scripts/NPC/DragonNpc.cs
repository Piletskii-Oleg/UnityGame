using UnityEngine;

namespace NPC
{
    public class DragonNpc : NPC
    {
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
    }
}