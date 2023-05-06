using System.Collections;
using Shared;
using UnityEngine;

namespace Enemy.Dragon
{
    public class DragonFire : MonoBehaviour
    {
        [SerializeField] private DragonFireSet fireSet;

        [SerializeField] private float fireStayTime;
        [SerializeField] private float fireSpeed;
        [SerializeField] private float fireFallSpeed;

        private float overallTimePassed;

        private LayerMask groundMask;
        private Collider[] collisions;

        private void Start()
        {
            StartCoroutine(MoveFire());
            
            collisions = new Collider[10];
            groundMask = 1 << LayerMask.NameToLayer("Ground");
        }

        private void Update()
        {
            overallTimePassed += Time.deltaTime;

            if (overallTimePassed > fireStayTime)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Actor>(out var actor))
            {
                fireSet.StepInFire(actor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            fireSet.StepOutOfFire();
        }

        private IEnumerator MoveFire()
        {
            var fireTransform = transform;
            while (Physics.OverlapSphereNonAlloc(fireTransform.position, 0.2f, collisions, groundMask) == 0)
            {
                fireTransform.Translate(fireTransform.rotation * fireTransform.forward * -(1
                    * Time.deltaTime * fireSpeed));

                fireTransform.Translate(Vector3.down * (Time.deltaTime * fireFallSpeed), Space.World);

                yield return null;
            }
        }
    }
}