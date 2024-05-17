// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    /// <summary>
    /// An easy component to be able to control the volume of a clip.
    /// </summary>
    [System.Serializable]
    public class VolumeController : MonoBehaviour {

        public enum AudioType {
            Sound,
            Ambience,
            Music
        }

        [SerializeField]
        private AudioType m_Type = AudioType.Sound;

        [SerializeField]
        private Gobblefish.UI.Slider m_Slider;

        void OnEnable() {
            SetSliderValue();
        }

        public void SetValue(float value) {
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

        public void SetSliderValue() {
            float value = 0f;
            switch (m_Type) {
                case AudioType.Sound:
                    value = AudioManager.Settings.soundVolume;
                    break;
                case AudioType.Ambience:
                    value = AudioManager.Settings.ambienceVolume;
                    break;
                case AudioType.Music:
                    value = AudioManager.Settings.musicVolume;
                    break;
            }
            if (m_Slider != null) {
                m_Slider.SetValue(value);
            }
        }
    
    }

}