// Unity.
using UnityEngine;

namespace GobbleFish.UI {

    using Audio;

    /// <summary>
    ///
    /// </summary>
    public class AudioVolumeCommands : MonoBehaviour {

        public enum AudioType {
            Sound,
            Ambience,
            Music
        }

        [SerializeField]
        private AudioType m_Type = AudioType.Sound;

        [HideInInspector]
        private Slider m_Slider;

        void Start() {
            m_Slider = GetComponent<Slider>();
            if (m_Slider != null) {
                m_Slider.SetValue(GetAudioVolume());
            }
        }

        public void SetAudioVolume(float value) {
            switch (m_Type) {
                case AudioType.Sound:
                    AudioManager.Settings.soundVolume = value;
                    break;
                case AudioType.Ambience:
                    AudioManager.Settings.ambienceVolume = value;
                    break;
                case AudioType.Music:
                    AudioManager.Settings.musicVolume = value;
                    break;
            }
            AudioManager.Settings.Save();
        }

        public float GetAudioVolume() {
            switch (m_Type) {
                case AudioType.Sound:
                    return AudioManager.Settings.soundVolume;
                case AudioType.Ambience:
                    return AudioManager.Settings.ambienceVolume;
                case AudioType.Music:
                    return AudioManager.Settings.musicVolume;
                default:
                    return 0f;
            }
        }
    
    }

}