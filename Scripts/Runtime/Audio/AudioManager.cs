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
    public sealed class AudioManager : Manager<AudioManager, AudioSettings> {

        public AudioSettings m_Settings;

        // The music being played in the game.
        [SerializeField]
        private MusicController m_Music;
        public MusicController Music => Instance.m_Music;
        
        // The ambience being played in the game.
        [SerializeField]
        private MusicController m_Ambience;
        public MusicController Ambience => Instance.m_Ambience;

        // The sounds being played in the game.
        [SerializeField]
        private SoundController m_Sounds;
        public SoundController Sounds => Instance.m_Sounds;

        // m_Music.Load(m_Settings.musicVolume  * 0.7f);
        // m_Ambience.Load(m_Settings.ambienceVolume * 0.3f);
        // m_Sounds.Load();

        private void Update() {
            m_Music.SetVolume(m_Settings.musicVolume * 0.7f * (m_Settings.musicMuted ? 0f : 1f));
            m_Ambience.SetVolume(m_Settings.ambienceVolume * 0.3f * (m_Settings.ambienceMuted ? 0f : 1f));
        }

    }

}

