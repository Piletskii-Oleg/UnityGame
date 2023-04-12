using UnityEngine;

namespace Functionality.Enemy.Slime
{
    /// <summary>
    /// Contains all possible face textures of the slime.
    /// </summary>
    [CreateAssetMenu(menuName = "Actor/Slime/Faces List")]
    public class SlimeFacesList : ScriptableObject
    {
        public Texture idleFace;
        public Texture jumpFace;
        public Texture walkFace;
        public Texture attackFace;
        public Texture damageFace;
    }
}