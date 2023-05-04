using UnityEngine;

namespace Enemy.Slime
{
    /// <summary>
    /// Contains all possible face textures of the slime.
    /// </summary>
    [CreateAssetMenu(menuName = "Actor/Slime/Faces List")]
    public class SlimeFacesList : ScriptableObject
    {
        public Texture idleFace;
        public Texture walkFace;
        public Texture attackFace;
        public Texture damageFace;
    }
}