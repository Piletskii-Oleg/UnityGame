using UnityEngine;

namespace Enemy.Slime
{
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