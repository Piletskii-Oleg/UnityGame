using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// Controls background music on the scene.
    /// </summary>
    public class BGMController : MonoBehaviour
    {
        [SerializeField] private MusicManager manager;
        [SerializeField] private float volumeDelta;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            PlayBackgroundMusic(audioSource.clip);
        }

        /// <summary>
        /// Starts playing background music fading in (and fades out already playing music).
        /// </summary>
        public void PlayBackgroundMusic(AudioClip clip)
            => StartCoroutine(StartBGM(clip));

        private IEnumerator StartBGM(AudioClip clip)
        {
            yield return StartCoroutine(StopBGM());
            
            audioSource.clip = clip;
            
            audioSource.volume = 0;
            
            audioSource.Play();
            
            while (audioSource.volume < manager.MusicVolume)
            {
                audioSource.volume += volumeDelta;
                yield return null;
            }
        }

        private IEnumerator StopBGM()
        {
            if (audioSource.isPlaying)
            {
                while (audioSource.volume > 0)
                {
                    audioSource.volume -= volumeDelta;
                    yield return null;
                }
            }
        }
    }
}
