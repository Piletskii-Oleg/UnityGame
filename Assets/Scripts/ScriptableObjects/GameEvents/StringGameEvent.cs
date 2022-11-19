using UnityEngine;

namespace ScriptableObjects.GameEvents
{
    /// <inheritdoc/>
    [CreateAssetMenu(fileName = "string", menuName = "Game Event/string")]
    public class StringGameEvent : BaseGameEvent<string>
    {
    }
}
