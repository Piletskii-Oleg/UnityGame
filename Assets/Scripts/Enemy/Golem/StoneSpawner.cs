using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Golem
{
    public class StoneSpawner
    {
        /// <summary>
        /// Instantiates and returns a game object from the list of <paramref name="stonePrefabs"/>
        /// with the specified <paramref name="parent"/>.
        /// </summary>
        /// <param name="stonePrefabs">A list of stone prefabs to choose from.</param>
        /// <param name="parent">Parent of the game object.</param>
        public static GameObject Spawn(List<GameObject> stonePrefabs, Transform parent)
            => Object.Instantiate(GetRandomStone(stonePrefabs), parent);

        /// <summary>
        /// Instantiates and returns a game object from the list of <paramref name="stonePrefabs"/>
        /// with the specified <paramref name="position"/> and <paramref name="rotation"/>.
        /// </summary>
        /// <param name="stonePrefabs">A list of stone prefabs to choose from.</param>
        /// <param name="position">Position of the instantiated game object.</param>
        /// <param name="rotation">Rotation of the instantiated game object.</param>
        public static GameObject Spawn(List<GameObject> stonePrefabs, Vector3 position, Quaternion rotation)
            => Object.Instantiate(GetRandomStone(stonePrefabs), position, rotation);

        private static GameObject GetRandomStone(IReadOnlyList<GameObject> stonePrefabs)
            => stonePrefabs[Random.Range(0, stonePrefabs.Count)];
    }
}