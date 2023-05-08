using UnityEngine;
using UnityEngine.Events;

namespace Enemy.Slime
{
    public class SlimeBoss : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStartBattle;

        public void OnStartBattle()
        {
            onStartBattle.Invoke();
        }
    }
}