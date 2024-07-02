// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gobblefish.UI {

    using TMPro;
    using Input;

    ///<summary>
    ///
    ///<summary>
    public class KeybindCommands : MonoBehaviour {

        public enum KeybindType {
            Direction,
            Action,
        }

        // The type of keybind this is.
        [SerializeField]
        private KeybindType m_Type = KeybindType.Direction;

        [SerializeField]
        private bool m_Active = false;

        // The keycode of this keybind.
        [SerializeField]
        private int m_KeyIndex;
        public KeyCode keyCode => GetKeyCode();

        [SerializeField]
        private Image m_TextBackground;

        [HideInInspector]
        private TextMeshProUGUI m_KeyText;

        [HideInInspector]
        protected KeybindGroup m_KeybindGroup;

        // Runs once before the first frame.
        void Start() {
            m_TextBackground.transform.SetParent(transform.parent);
            foreach (Transform child in m_TextBackground.transform) {
                TextMeshProUGUI textMesh = child.GetComponent<TextMeshProUGUI>();
                if (textMesh != null) {
                    m_KeyText = textMesh;
                    m_KeyText.text = GetKeyCode().ToString();
                    break;
                }
            }
        }

        void Update() {
            if (m_Active) {
                CheckReassignKey();
            }
        }

        public void Activate(bool activate) {
            m_Active = activate;
            if (m_Active) {
                // Disable all other keybinds.
                foreach (KeybindCommands keybind in m_KeybindGroup.Keybinds) {
                    if (keybind != this) {
                        keybind.Activate(false);
                    }
                }
            }
        }

        // Set the keybind group that this is a part of.
        public void SetKeybindGroup(KeybindGroup keybindGroup) {
            m_KeybindGroup = keybindGroup;
        }

        public KeyCode GetKeyCode() {
            // Make sure there is an input system.
            if (InputSystem.Instance == null) { 
                return KeyCode.None; 
            }

            // Get the current keybind for this key index.
            KeyCode[] keyCodes = GetKeycodesArray();
            if (keyCodes == null || m_KeyIndex < 0 || m_KeyIndex >= keyCodes.Length) {
                return KeyCode.None;
            }
            return keyCodes[m_KeyIndex];
        }

        public KeyCode[] GetKeycodesArray() {
            return m_Type switch {
                KeybindType.Direction => InputSystem.Settings.directionKeybinds,
                KeybindType.Action => InputSystem.Settings.actionKeybinds,
                _ => null
            };
        }

        public void CheckReassignKey(){

            // Ignore frames without keys
            if (UnityEngine.Input.inputString.Length <= 0) { 
                return;
            }

            // Input.inputString stores all keys pressed.
            ReassignKey((KeyCode)UnityEngine.Input.inputString[0]);

            // Check all the other keybinds in case they have the same key.
            foreach (KeybindCommands keybind in m_KeybindGroup.Keybinds) {
                if (keybind != this && keybind.keyCode == keyCode && keyCode != KeyCode.None) {
                    keybind.ReassignKey(KeyCode.None);
                }
            }
        
        }

        public void ReassignKey(KeyCode key) {
            // Make sure there is an input system.
            if (InputSystem.Instance == null) { 
                return; 
            }

            // Set the keycode in the array.
            KeyCode[] keyCodes = GetKeycodesArray();
            if (keyCodes == null || m_KeyIndex < 0 || m_KeyIndex >= keyCodes.Length) {
                return;
            }

            KeyCode originalKey = keyCodes[m_KeyIndex];
            keyCodes[m_KeyIndex] = key;
            InputSystem.Settings.Save();

            // Update the UI.
            m_KeyText.text = key.ToString();
            Activate(false);

            // Print a debug message.
            Debug.Log("Reassigned key '" + originalKey + "' to key '" + key + "'");

        }

    }

}

