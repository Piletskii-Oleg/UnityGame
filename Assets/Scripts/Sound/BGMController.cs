using System.Collections;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// Controls background music on the scene.
    /// </summary>
    public class BGMController : MonoBehaviour
    {
        [SerializeField] private MusicManager manager;
    
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            PlayBackgroundMusic();
        }

        /// <summary>
        /// Starts playing background music fading in.
        /// </summary>
        public void PlayBackgroundMusic()
            => StartCoroutine(StartBGM());

        private IEnumerator StartBGM()
        {
            audioSource.volume = 0;
            audioSource.Play();
            while (audioSource.volume < manager.MusicVolume)
            {
                audioSource.volume += 0.002f;
                yield return null;
            }
        }
    }
}
