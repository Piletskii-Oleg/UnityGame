using System.Collections.Generic;
using UnityEngine;

namespace Core.Enemy
{
    /// <summary>
    /// Helper class that spawns random game objects from a given list of game objects.
    /// </summary>
    public static class GameObjectSpawner
    {
        /// <summary>
        /// Instantiates and returns a game object from the list of <paramref name="prefabs"/>
        /// with the specified <paramref name="parent"/>.
        /// </summary>
        /// <param name="prefabs">A list of prefabs to choose from.</param>
        /// <param name="parent">Parent of the game object.</param>
        public static GameObject Spawn(List<GameObject> prefabs, Transform parent)
            => Object.Instantiate(GetRandom(prefabs), parent);

        /// <summary>
        /// Instantiates and returns a game object from the list of <paramref name="prefabs"/>
        /// with the specified <paramref name="position"/> and <paramref name="rotation"/>.
        /// </summary>
        /// <param name="prefabs">A list of prefabs to choose from.</param>
        /// <param name="position">Position of the instantiated game object.</param>
        /// <param name="rotation">Rotation of the instantiated game object.</param>
        public static GameObject Spawn(List<GameObject> prefabs, Vector3 position, Quaternion rotation)
            => Object.Instantiate(GetRandom(prefabs), position, rotation);

        private static GameObject GetRandom(IReadOnlyList<GameObject> prefabs)
            => prefabs[Random.Range(0, prefabs.Count)];
    }
}