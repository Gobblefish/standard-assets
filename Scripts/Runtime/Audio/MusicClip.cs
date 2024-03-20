// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    [CreateAssetMenu(fileName="Music Clip", menuName="Music Clip")]
    public class MusicClip : ScriptableObject {

        [SerializeField]
        private AudioClip m_IntroSection;

        [SerializeField]
        private AudioClip[] m_Sections;

        public void Play() {
            if (AudioManager.Instance == null) { return; }
            AudioManager.Music.Play(m_IntroSection);
            //
        }

    }

}