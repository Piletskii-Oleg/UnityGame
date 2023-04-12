using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Spider
{
    /// <summary>
    /// Put on a game object that spawns spiders.
    /// </summary>
    public class SpiderSpawner : MobSpawner
    {
        [Header("Exit")]
        [Tooltip("Object that spider should go to when spawned")]
        [SerializeField] private Transform exitTransform;

        protected override void SpawnEnemy()
        {
            base.SpawnEnemy();
            
            var enemyScript = enemies[^1].GetComponent<Spider>();
            enemyScript.SetArea(area);
            enemyScript.GetSpawned(exitTransform.position);
        }
    }
}