// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    ///<summary>
    /// Ties the audio functionality to the rest of the game.
    ///<summary>
    public sealed class AudioManager : Manager<AudioManager, AudioSettings> {

        // The music being played in the game.
        private MusicController m_Music;
        public static MusicController Music => Instance.m_Music;
        
        // The ambience being played in the game.
        private MusicController m_Ambience;
        public static MusicController Ambience => Instance.m_Ambience;

        // The sounds being played in the game.
        private SoundController m_Sounds;
        public static SoundController Sounds => Instance.m_Sounds;

        // m_Music.Load(m_Settings.musicVolume  * 0.7f);
        // m_Ambience.Load(m_Settings.ambienceVolume * 0.3f);
        // m_Sounds.Load();

        protected override void Awake() {
            m_Settings = new AudioSettings();
            m_Music = new MusicController("Music", 0f, transform);
            m_Ambience = new MusicController("Ambience", 0f, transform);
            m_Sounds = new SoundController("Sounds", transform);
            base.Awake();
        }

        private void Update() {
            m_Music.SetVolume(Settings.musicVolume * 0.7f * (Settings.musicMuted ? 0f : 1f));
            m_Ambience.SetVolume(Settings.ambienceVolume * 0.3f * (Settings.ambienceMuted ? 0f : 1f));
        }

    }

}