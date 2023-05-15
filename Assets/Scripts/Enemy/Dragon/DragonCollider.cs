using UnityEngine;

namespace Enemy.Dragon
{
    /// <summary>
    /// Component for a collider that is attached to a different game object from
    /// the one where <see cref="Dragon"/> script is.
    /// </summary>
    public class DragonCollider : MonoBehaviour
    {
        [SerializeField] private Dragon dragon;
        
        private void OnCollisionEnter(Collision other)
        {
            dragon.OnCollide(other);
        }
    }
}