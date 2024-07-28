// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Audio {

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

        protected override void Awake() {
            m_Settings = new AudioSettings();
            m_Music = new MusicController("Music", 0f, transform);
            m_Ambience = new MusicController("Ambience", 0f, transform);
            m_Sounds = new SoundController("Sounds", transform);
            base.Awake();
        }

        private void Update() {
            m_Music.SetVolume(Settings.musicVolume * 0.7f * (Settings.musicMuted ? 0f : 1f));
            m_Music.CheckQueue();
            m_Ambience.SetVolume(Settings.ambienceVolume * 0.3f * (Settings.ambienceMuted ? 0f : 1f));
        }

    }

}