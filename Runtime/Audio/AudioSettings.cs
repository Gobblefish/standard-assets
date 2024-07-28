// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Audio {

    [System.Serializable]
    public class AudioSettings : Settings<AudioSettings> {
        
        // Master Volume.
        public float masterVolume;
        public bool masterMuted;
        
        // Music Volume.
        public float musicVolume;
        public bool musicMuted;
        
        // Ambience Volume.
        public float ambienceVolume;
        public bool ambienceMuted;
        
        // Sound Volume.
        public float soundVolume;
        public bool soundMuted;
        
    }

}