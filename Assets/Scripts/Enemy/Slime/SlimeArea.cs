using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Slime
{
    /// <summary>
    /// Area in which slime operates.
    /// </summary>
    public class SlimeArea : MonoBehaviour
    {
        [Tooltip("Radius of a circle in which slime can roam freely")]
        [SerializeField] private float radius;

        /// <summary>
        /// Gets next position that slime will go to.
        /// </summary>
        /// <returns>A position that slime will go to.</returns>
        public Vector3 GetNewPosition()
        {
            float distance = radius * Mathf.Sqrt(Random.value);
            var angle = Random.value * 2 * Mathf.PI;
            var position = transform.position;
            return new Vector3(position.x + distance * Mathf.Cos(angle), 
                position.y, position.z + distance * Mathf.Sin(angle));
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}