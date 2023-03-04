using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Slime
{
    public class SlimeArea : MonoBehaviour
    {
        [SerializeField] private float radius;

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