using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Spider
{
    public class SpiderSpawner : MonoBehaviour
    {
        private readonly List<GameObject> spiders = new ();

        private float timePassed;
        
        [Header("Spawner Stats")]
        [Tooltip("Max amount of enemies around the spawner")]
        [SerializeField] private int maxEnemiesAmount;
        [Tooltip("Prefab that contains the spider")]
        [SerializeField] private GameObject spiderPrefab;
        [Tooltip("Area that corresponds to that spawner")]
        [SerializeField] private CircleArea area;
        [Tooltip("Object that spider should go to when spawned")]
        [SerializeField] private Transform exitTransform;
        [Tooltip("Cooldown between spawns")]
        [SerializeField] private float spawnCooldown;

        [Header("Player")]
        [SerializeField] private Transform playerTransform;

        private void Update()
        {
            timePassed += Time.deltaTime;
            if (!area.IsPositionInsideActiveRadius(playerTransform.position) || spiders.Count > maxEnemiesAmount)
            {
                return;
            }

            if (timePassed > spawnCooldown)
            {
                timePassed = 0;
                
                var spider = Instantiate(spiderPrefab, transform.position, Quaternion.identity);

                spiders.Add(spider);

                var spiderScript = spider.GetComponent<Spider>();

                spiderScript.SetArea(area);
                spiderScript.GetSpawned(exitTransform.position);
            }
        }
    }
}