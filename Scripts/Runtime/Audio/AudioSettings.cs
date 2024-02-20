// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    [System.Serializable]
    public sealed class AudioSettings : Gobblefish.Settings<AudioSettings> {
        
        public const string AUDIO_SETTINGS_FILE_PATH = "/settings/audio";

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