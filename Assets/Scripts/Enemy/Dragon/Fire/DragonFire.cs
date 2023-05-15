using System;
using System.Collections;
using Shared;
using UnityEngine;

namespace Enemy.Dragon.Fire
{
    public class DragonFire : MonoBehaviour
    {
        [SerializeField] private DragonFireSet fireSet;

        [Tooltip("Time in seconds after which the fire disappears")]
        [SerializeField] private float fireStayTime;
        
        [Tooltip("Speed at which fire will move forward relative to the dragon")]
        [SerializeField] private float fireSpeed;
        
        [Tooltip("Speed at which fire will fall at the ground")]
        [SerializeField] private float fireFallSpeed;

        private bool isTouching;
        
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
                isTouching = true;
                fireSet.StepInFire(actor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (isTouching)
            {
                fireSet.StepOutOfFire();
                isTouching = false;
            }
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

        private void OnDisable()
        {
            if (isTouching)
            {
                fireSet.StepOutOfFire();
                isTouching = false;
            }
        }
    }
}