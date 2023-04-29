using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    /// <summary>
    /// Area in which an enemy can operate.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        private SphereCollider sphereCollider;
        
        [FormerlySerializedAs("radius")]
        [Tooltip("Radius of a circle in which enemy can roam freely")]
        [SerializeField] private float enemyRoamRadius;

        [Tooltip("Radius of a circle in which player should be for area to be active")]
        [SerializeField] private float playerViewRadius;

        [SerializeField] private UnityEvent onAreaEnter;
        [SerializeField] private UnityEvent onAreaExit;

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

        /// <summary>
        /// Tells if the specified position is contained in the sphere of player visibility.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns></returns>
        public bool IsPositionInsideActiveRadius(Vector3 position)
            => (transform.position - position).magnitude < playerViewRadius;

        private void Start()
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = enemyRoamRadius;
            sphereCollider.isTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("exit");
                onAreaExit.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("enter");
                onAreaEnter.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            // Gizmos.DrawWireSphere(transform.position, enemyRoamRadius);
        }
    }
}