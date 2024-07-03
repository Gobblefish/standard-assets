// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    [CreateAssetMenu(fileName="MusicClip", menuName="MusicClip")]
    public class MusicClip : ScriptableObject {

        [SerializeField]
        private AudioClip m_IntroSection;

        [SerializeField]
        private AudioClip[] m_Sections;

        [SerializeField]
        private int BPM;

        [SerializeField]
        private List<int> queue;

        public void Play() {
            if (AudioManager.Instance == null) { return; }
            AudioManager.Music.Play(m_IntroSection);
            for (int i = 0; i < queue.Count; i++) {
                if (queue[i] < m_Sections.Length) {
                    AudioManager.Music.Queue(m_Sections[queue[i]]);
                }
            }
        }

    }

}