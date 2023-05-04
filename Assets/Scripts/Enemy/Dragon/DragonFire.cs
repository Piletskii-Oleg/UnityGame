using System.Collections;
using DG.Tweening;
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

        private void Start()
        {
            StartCoroutine(MoveFire());
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
            const float groundLevel = 51;
            var fireTransform = transform;
            while (fireTransform.position.y - groundLevel > Mathf.Epsilon)
            {
                fireTransform.Translate(fireTransform.rotation * fireTransform.forward * -(1
                    * Time.deltaTime * fireSpeed));
                fireTransform.DOMoveY(fireTransform.position.y - fireFallSpeed * Time.deltaTime, Time.deltaTime);
                yield return null;
            }
        }
    }
}