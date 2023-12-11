// Unity.
using UnityEngine;

namespace Gobblefish.Audio {

    /// <summary>
    /// An easy component to be able to control the volume of a clip.
    /// </summary>
    public class AudioSnippetCollection : MonoBehaviour {

        [SerializeField]
        private AudioSnippet[] m_Snippets;

        public void Play(string name) {
            for (int i = 0; i < m_Snippets.Length; i++) {
                if (m_Snippets[i].name == name) {
                    m_Snippets[i].Play();       
                }
            }
        }
    
    }

}