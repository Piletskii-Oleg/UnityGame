using System.Collections.Generic;
using Player.ScriptableObjects;
using UnityEngine;

namespace Enemy
{
    public class MobSpawner : MonoBehaviour
    {
        protected readonly List<GameObject> enemies = new ();
        
        private float timePassed;
        
        [Header("Player")]
        [SerializeField]
        protected PlayerScriptableObject playerScriptableObject;
        
        [Header("Spawner Stats")]
        [Tooltip("Max amount of enemies around the spawner")]
        [SerializeField]
        protected int maxEnemiesAmount;

        [Tooltip("Prefab that contains the enemy")]
        [SerializeField] private GameObject enemyPrefab;

        [Tooltip("Area that corresponds to that spawner")]
        [SerializeField]
        protected CircleArea area;

        [Tooltip("Cooldown between spawns")]
        [SerializeField]
        protected float spawnCooldown;

        private void Update()
        {
            if (enemies.Count > maxEnemiesAmount)
            {
                return;
            }
            
            timePassed += Time.deltaTime;

            if (timePassed > spawnCooldown)
            {
                timePassed = 0;
                if (!area.IsPositionInsideActiveRadius(playerScriptableObject.GetActualPlayerPosition()))
                {
                    RemoveLast();
                }
                else
                {
                    SpawnEnemy();
                }
            }
        }
        
        private void RemoveLast()
        {
            if (enemies.Count == 0)
            {
                return;
            }
            
            Destroy(enemies[0]);
            enemies.RemoveAt(0);
        }

        protected virtual void SpawnEnemy()
        {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            enemies.Add(enemy);
        }
    }
}