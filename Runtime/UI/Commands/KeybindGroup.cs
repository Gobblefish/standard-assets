// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.UI {

    public class KeybindGroup : MonoBehaviour {

        // The keybind buttons under this transform.
        [SerializeField]
        private List<KeybindCommands> m_Keybinds;
        public List<KeybindCommands> Keybinds => m_Keybinds;

        // Runs once before the first frame.
        void Start() {
            m_Keybinds = new List<KeybindCommands>();
            GetKeybind(transform, ref m_Keybinds);
        }

        // Recursively collect all keybind controllers under this transform.
        private void GetKeybind(Transform transform, ref List<KeybindCommands> keybinds) {
            KeybindCommands keybind = null;
            foreach (Transform child in transform) {
                keybind = child.GetComponent<KeybindCommands>();
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

