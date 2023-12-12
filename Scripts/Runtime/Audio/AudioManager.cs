// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Audio;

namespace Gobblefish.Audio {

    ///<summary>
    /// Ties the audio functionality to the rest of the game.
    ///<summary>
    public class AudioManager : MonoBehaviour {

        // The singleton.
        private static AudioManager INSTANCE; 

        [SerializeField]
        private AudioSettings m_Settings;
        public AudioSettings Settings => m_Settings;
        public static AudioSettings SETTINGS => INSTANCE.m_Settings;

        // The music being played in the game.
        [SerializeField]
        private MusicController m_Music;
        public MusicController Music => INSTANCE.m_Music;
        public static MusicController MUSIC => INSTANCE.m_Music;
        
        // The ambience being played in the game.
        [SerializeField]
        private MusicController m_Ambience;
        public MusicController Ambience => m_Ambience;
        public static MusicController AMBIENCE => INSTANCE.m_Ambience;

        // The sounds being played in the game.
        [SerializeField]
        private SoundController m_Sounds;
        public SoundController Sounds => m_Sounds;
        public static SoundController SOUNDS => INSTANCE.m_Sounds;

        public void SetSettings(AudioSettings settings) {
            m_Settings = settings;
        }

        void Awake() {
            INSTANCE = this;
            m_Music.Load(m_Settings.musicVolume  * 0.7f);
            m_Ambience.Load(m_Settings.ambienceVolume * 0.3f);
            m_Sounds.Load();
        }

        private void Update() {

            m_Music.SetVolume(m_Settings.musicVolume * 0.7f * (m_Settings.musicMuted ? 0f : 1f));
            m_Ambience.SetVolume(m_Settings.ambienceVolume * 0.3f * (m_Settings.ambienceMuted ? 0f : 1f));

        }

    }

}

