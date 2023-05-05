using UnityEngine;

namespace Enemy.Dragon
{
    public class DragonCollider : MonoBehaviour
    {
        [SerializeField] private Dragon dragon;
        
        private void OnCollisionEnter(Collision other)
        {
            dragon.OnCollide(other);
        }
    }
}