using UnityEngine;
using UnityEngine.Events;

namespace Enemy.Slime
{
    /// <summary>
    /// Component for the slime boss. Only used to start the boss music.
    /// </summary>
    public class SlimeBoss : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStartBattle;

        public void OnStartBattle()
        {
            onStartBattle.Invoke();
        }
    }
}