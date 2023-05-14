using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class CircleArea : MonoBehaviour
    {
        [FormerlySerializedAs("radius")]
        [Tooltip("Radius of a circle in which enemy can roam freely")]
        [SerializeField] protected float enemyRoamRadius;

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
        /// Gets next position that enemy will go to (within a given radius).
        /// </summary>
        /// <returns>A position that enemy will go to.</returns>
        /// <param name="angle">Angle of the point.</param>
        public Vector3 GetPositionOnCircle(float angle)
            => GetPositionOnCircle(enemyRoamRadius, transform.position, angle);

        /// <summary>
        /// Gets next position that enemy will go to (within a given radius).
        /// </summary>
        /// <returns>A position that enemy will go to.</returns>
        /// <param name="radius">Radius in which point is selected.</param>
        /// <param name="centerPosition">A point around which the destination point is selected.</param>
        public static Vector3 GetPositionOnCircle(float radius, Vector3 centerPosition)
            => GetPositionOnCircle(radius, centerPosition, Random.value * 2 * Mathf.PI);

        /// <summary>
        /// Gets next position that enemy will go to (within a given radius).
        /// </summary>
        /// <returns>A position that enemy will go to.</returns>
        /// <param name="radius">Radius in which point is selected.</param>
        /// <param name="centerPosition">A point around which the destination point is selected.</param>
        /// <param name="angle">Angle of the point.</param>
        public static Vector3 GetPositionOnCircle(float radius, Vector3 centerPosition, float angle)
            => new(centerPosition.x + radius * Mathf.Cos(angle), 
                centerPosition.y,
                centerPosition.z + radius * Mathf.Sin(angle));

        /// <summary>
        /// Gets next position within the area that enemy will go to.
        /// </summary>
        /// <returns>A position that enemy will go to.</returns>
        public Vector3 GetNewPosition()
            => GetNewPosition(enemyRoamRadius, transform.position);
        
        private void OnDrawGizmos()
        {
            // Gizmos.DrawWireSphere(transform.position, enemyRoamRadius);
        }
    }
}