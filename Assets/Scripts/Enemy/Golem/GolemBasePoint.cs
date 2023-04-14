using UnityEngine;

namespace Enemy.Golem
{
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