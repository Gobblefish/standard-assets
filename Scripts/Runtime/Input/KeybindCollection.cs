// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Input;

namespace Gobblefish.Input {

    public class KeybindCollection : MonoBehaviour {

        // The keybind buttons under this transform.
        [SerializeField]
        private List<KeybindController> m_Keybinds;
        public List<KeybindController> Keybinds => m_Keybinds;

        // Runs once before the first frame.
        void Start() {
            m_Keybinds = new List<KeybindController>();
            GetKeybind(transform, ref m_Keybinds);
        }

        // Recursively collect all keybind controllers under this transform.
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

