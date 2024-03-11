/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    public static class AudioExtensions {
        
        public static void Play(this AudioClip clip, float volume = 1f) {
            AudioManager.Sounds.PlaySound(clip, volume);
        }

    }

}
