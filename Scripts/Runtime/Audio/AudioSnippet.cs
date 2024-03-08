// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    /// <summary>
    /// An easy component to be able to control the volume of a clip.
    /// </summary>
    [System.Serializable]
    public class AudioSnippet {

        // The name of the clip.
        public string name = "untitled";

        // The clip attached to this snippet.    
        public AudioClip clip;
        
        // The volume to play the audio at.
        [Range(0f, 1f)] 
        public float volume = 1f;
        
        // The priority of this audio.
        public int priority = 0;

        public void Play() {
            AudioManager.Sounds.PlaySnippet(this);
        }

        public void Stop() {
            AudioManager.Sounds.StopSound(clip);
        }
    
    }

}