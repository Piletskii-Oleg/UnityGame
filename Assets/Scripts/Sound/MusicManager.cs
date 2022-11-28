using UnityEngine;

namespace Sound
{
    /// <summary>
    /// Stores and manages information about music and sound effects.
    /// </summary>
    [CreateAssetMenu(fileName = "Music Manager", menuName = "Managers/Music Manager")]
    public class MusicManager : ScriptableObject
    {
        [field: Range(0, 1f)]
        [field:SerializeField] public float MusicVolume { get; private set; }
        
        [field: Range(0, 1f)]
        [field:SerializeField] public float SoundVolume { get; private set; }
    }
}