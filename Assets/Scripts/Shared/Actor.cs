using System;
using System.Collections;
using Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Shared
{
    /// <summary>
    /// Basic class for an actor (player, enemy etc.) in the world.
    /// </summary>
    public class Actor : MonoBehaviour // abstract
    {
        protected Animator animator;
        
        [Header("Data")]
        [SerializeField] protected ActorData actorData;
        
        [Header("Events")]
        [SerializeField] protected UnityEvent<float> onTakeDamage;
        [SerializeField] private UnityEvent onKill;

        public string Name => actorData.name;

        /// <summary>
        /// Used when the actor takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        /// <param name="actorAffiliation">Affiliation of the actor that tries to inflict damage.</param>
        public virtual void OnTakeDamage(float damage, ActorAffiliation actorAffiliation)
        {
            if (actorData.affiliation.enemyFractions.Contains(actorAffiliation))
            {
                onTakeDamage.Invoke(damage);
            }
        }

        /// <summary>
        /// Used when the actor is killed.
        /// </summary>
        public virtual void OnKill()
        {
            onKill.Invoke();
        }

        /// <summary>
        /// Sets a value of an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        /// <param name="value">Value to set.</param>
        public void SetAnimationValue(int animationHash, float value)
            => animator.SetFloat(animationHash, value);

        /// <summary>
        /// Sets a value of an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        /// <param name="value">Value to set.</param>
        public void SetAnimationValue(int animationHash, bool value)
            => animator.SetBool(animationHash, value);

        /// <summary>
        /// Sets a value of an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        /// <param name="value">Value to set.</param>
        public void SetAnimationValue(int animationHash, int value)
            => animator.SetInteger(animationHash, value);

        /// <summary>
        /// Triggers an animation variable.
        /// </summary>
        /// <param name="animationHash">Hash that corresponds to some animation variable.</param>
        public void TriggerAnimation(int animationHash)
            => animator.SetTrigger(animationHash);
        
        public void InvokeAfterSeconds<T>(Func<T> function, float seconds)
            => StartCoroutine(InvokeAfterSecondsCoroutine(function, seconds));

        private static IEnumerator InvokeAfterSecondsCoroutine<T>(Func<T> function, float seconds)
        {
            float timePassed = 0;
            while (timePassed < seconds)
            {
                timePassed += Time.deltaTime;
                yield return null;
            }

            function.Invoke();
        }
        
        protected static Vector3 GenerateRandomVector(float minValue = 0f, float maxValue = 10f)
        {
            float valueX = Random.Range(minValue, maxValue);
            float valueY = Random.Range(minValue, maxValue);
            float valueZ = Random.Range(minValue, maxValue);
            return new Vector3(valueX, valueY, valueZ);
        }
    }
}
