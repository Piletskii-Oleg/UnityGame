using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    /// <summary>
    /// Area in which an actor can operate.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        [FormerlySerializedAs("radius")]
        [Tooltip("Radius of a circle in which enemy can roam freely")]
        [SerializeField] private float enemyRoamRadius;

        [Tooltip("Radius of a circle in which player should be for area to be active")]
        [SerializeField] private float playerViewRadius;

        /// <summary>
        /// Gets next position that enemy will go to.
        /// </summary>
        /// <returns>A position that enemy will go to.</returns>
        public Vector3 GetNewPosition()
        {
            float distance = enemyRoamRadius * Mathf.Sqrt(Random.value);
            
            float angle = Random.value * 2 * Mathf.PI;
            
            var position = transform.position;
            
            return new Vector3(position.x + distance * Mathf.Cos(angle), 
                position.y, position.z + distance * Mathf.Sin(angle));
        }

        /// <summary>
        /// Tells if the specified position is contained in the sphere around 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsPositionInsideActiveRadius(Vector3 position)
            => (transform.position - position).magnitude < playerViewRadius;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, enemyRoamRadius);
        }
    }
}