using UnityEngine;

namespace Enemy.Golem
{
    /// <summary>
    /// Component for the base point of the golem.
    /// </summary>
    public class GolemBasePoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Golem>(out var golem))
            {
                if (golem.CheckBasePosition(transform))
                {
                    golem.Stop();
                    golem.TurnToBaseAngle();
                }
            }
        }
    }
}