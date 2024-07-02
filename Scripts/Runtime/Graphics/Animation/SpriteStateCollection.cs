// Unity.
using UnityEngine;
using UnityEngine.UI;
// Gobblefish.
using Gobblefish.Animation;

namespace Gobblefish.Animation {

    /// <summary>
    /// 
    /// </summary>
    public class SpriteStateCollection : MonoBehaviour {

        [SerializeField]
        private SpriteState[] m_States;

        public void Set(Image image, string name) {
            for (int i = 0; i < m_States.Length; i++) {
                if (m_States[i].name == name) {
                    image.sprite = m_States[i].sprite;      
                }
            }
        }

        public void Set(SpriteRenderer spriteRenderer, string name) {
            for (int i = 0; i < m_States.Length; i++) {
                if (m_States[i].name == name) {
                    spriteRenderer.sprite = m_States[i].sprite;    
                }
            }
        }
    
    }

}