using DG.Tweening;
using UnityEngine;

namespace NPC
{
    public class Robot : NPC
    {
        [Tooltip("Transform at which Engie looks after finishing conversation with the player")]
        [SerializeField] private Transform lookAt;

        [Header("Engie elements")]
        [SerializeField] private Transform headTransform;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public override void StartConversation()
        {
            base.StartConversation();

            var newPosition = CalculateReversePosition(playerScriptableObject.GetActualPlayerPosition());

            headTransform.DOLookAt(newPosition, 1f);
        }

        private Vector3 CalculateReversePosition(Vector3 position) // this is required because robots' heads are reversed
            => position - 2 * (position - headTransform.position);

        public void OnEndConversation()
            => headTransform.DOLookAt(CalculateReversePosition(lookAt.position), 1f);
    }
}