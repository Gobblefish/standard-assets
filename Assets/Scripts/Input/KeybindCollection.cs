// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Input.Keyboard;

namespace Gobblefish.Input.Keyboard {

    public class KeybindCollection : MonoBehaviour {

        // The keybind buttons under this transform.
        [SerializeField]
        private List<KeybindController> m_Keybinds;
        public List<KeybindController> Keybinds => m_Keybinds;

        void Start() {
            
            m_Keybinds = new List<KeybindController>();
            GetKeybind(transform, ref m_Keybinds);

        }

        private void GetKeybind(Transform transform, ref List<KeybindController> keybinds) {
            KeybindController keybind = null;
            foreach (Transform child in transform) {
                keybind = child.GetComponent<KeybindController>();
                if (keybind != null) {
                    m_Keybinds.Add(keybind);
                    keybind.SetKeybindGroup(this);
                }
                else {
                    GetKeybind(child, ref keybinds);
                }
            }
        }

    }

}

