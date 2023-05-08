using System.Collections.Generic;
using Player.ScriptableObjects;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Area in which an enemy can operate.
    /// </summary>
    public class MobArea : CircleAreaBase
    {
        private readonly List<GameObject> enemies = new();

        [Header("Player")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Tooltip("Radius of a circle in which player should be for area to be active")]
        [SerializeField] private float playerViewRadius;

        [Header("Spawner Stats")]
        [Tooltip("Max amount of enemies around the spawner")]
        [SerializeField] private int maxEnemiesAmount;

        [Tooltip("Min amount of enemies around the spawner")]
        [SerializeField] private int minEnemiesAmount;

        [Tooltip("Cooldown between spawns (in seconds)")]
        [SerializeField] private float spawnCooldown;
        
        [Tooltip("Prefab that contains the enemy")]
        [SerializeField] private List<GameObject> enemyPrefabs;

        [SerializeField] private List<Transform> spawnPoints;

        private float timePassed;

        private void Update()
        {
            timePassed += Time.deltaTime;

            if (timePassed > spawnCooldown)
            {
                TrySpawn();
                timePassed = 0;
            }
        }
        
        /// <summary>
        /// Tells if the specified position is contained in the sphere of player visibility.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns></returns>
        private bool IsPositionInsideActiveRadius(Vector3 position)
            => (transform.position - position).magnitude < playerViewRadius;

        private void TrySpawn()
        {
            if (enemies.Count > maxEnemiesAmount)
            {
                return;
            }

            if (IsPositionInsideActiveRadius(playerScriptableObject.GetActualPlayerPosition()))
            {
                SpawnEnemy();
            }
            else if (enemies.Count > minEnemiesAmount)
            {
                RemoveLast();
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

        private void SpawnEnemy()
        {
            var spawner = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];
            var enemy = GameObjectSpawner.Spawn(enemyPrefabs, spawner.position, Quaternion.identity);

            enemies.Add(enemy);
            
            var enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.SetArea(this);
        }
    }
}