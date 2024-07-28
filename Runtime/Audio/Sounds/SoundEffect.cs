// Unity.
using UnityEngine;

namespace GobbleFish.Audio {

    /// <summary>
    /// An easy component to be able to control the volume of a clip.
    /// </summary>
    [System.Serializable]
    public class SoundEffect {

        public AudioClip clip;

        // The priority of this audio.
        public int priority = 0;
        
        // The volume to play the audio at.
        [Range(0f, 1f)] 
        public float volume = 1f;

        public void Play() {
            if (AudioManager.Instance != null) {
                AudioManager.Sounds.PlaySound(this);
            }
        }

        public void Stop() {
            if (AudioManager.Instance != null) {
                AudioManager.Sounds.StopSound(clip);
            }
        }
    
    }

}