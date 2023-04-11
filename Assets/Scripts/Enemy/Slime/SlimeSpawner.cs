using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Slime
{
    public class SlimeSpawner : MobSpawner
    {
        [SerializeField] private List<GameObject> slimePrefabs;
        
        protected override void SpawnEnemy()
        {
            var enemy = GameObjectSpawner.Spawn(slimePrefabs, transform.position, Quaternion.identity);
            
            enemies.Add(enemy);

            var slime = enemies[^1];
            var slimeScript = slime.GetComponent<Slime>();
            slimeScript.SetArea(area);
        }
    }
}