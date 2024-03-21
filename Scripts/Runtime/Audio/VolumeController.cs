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

        public void SetValue(float value) {
            switch (m_Type) {
                case AudioType.Sound:
                    AudioManager.Settings.soundVolume = value;
                    return;
                case AudioType.Ambience:
                    AudioManager.Settings.ambienceVolume = value;
                    return;
                case AudioType.Music:
                    AudioManager.Settings.musicVolume = value;
                    return;
            }
        }
    
    }

}