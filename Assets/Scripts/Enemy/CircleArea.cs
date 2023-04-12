using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    /// <summary>
    /// Area in which an enemy can operate.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        [FormerlySerializedAs("radius")]
        [Tooltip("Radius of a circle in which enemy can roam freely")]
        [SerializeField] private float enemyRoamRadius;

        [Tooltip("Radius of a circle in which player should be for area to be active")]
        [SerializeField] private float playerViewRadius;

        /// <summary>
        /// Gets next position that enemy will go to (within a given radius).
        /// </summary>
        /// <returns>A position that enemy will go to.</returns>
        /// <param name="radius">Radius in which point is selected.</param>
        /// <param name="centerPosition">A point around which the destination point is selected.</param>
        public static Vector3 GetNewPosition(float radius, Vector3 centerPosition)
        {
            float distance = radius * Mathf.Sqrt(Random.value);
            
            float angle = Random.value * 2 * Mathf.PI;

            return new Vector3(centerPosition.x + distance * Mathf.Cos(angle), 
                centerPosition.y, centerPosition.z + distance * Mathf.Sin(angle));
        }
        
        /// <summary>
        /// Gets next position within the area that enemy will go to.
        /// </summary>
        /// <returns>A position that enemy will go to.</returns>
        public Vector3 GetNewPosition()
            => GetNewPosition(enemyRoamRadius, transform.position);

        /// <summary>
        /// Tells if the specified position is contained in the sphere of player visibility.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns></returns>
        public bool IsPositionInsideActiveRadius(Vector3 position)
            => (transform.position - position).magnitude < playerViewRadius;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, enemyRoamRadius);
        }
    }
}