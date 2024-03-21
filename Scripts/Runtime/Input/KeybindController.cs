// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
// Gobblefish.
using Gobblefish.Input;

namespace Gobblefish.Input {

    public class KeybindController : MonoBehaviour {

        public enum KeybindType {
            Direction,
            Action,
        }

        //
        [SerializeField]
        private KeybindType m_Type = KeybindType.Direction;

        // If the controller is actively being assigned.
        [SerializeField]
        private bool m_Active = false;

        // The keycode of this keybind.
        [SerializeField]
        private int m_KeyIndex;
        private KeyCode Key => GetKeybind();

        [SerializeField]
        private TextMeshProUGUI m_TextMesh;

        [SerializeField]
        private Image m_Image;

        // The keybind collection that this is a part of.
        protected KeybindCollection m_KeybindCollection;

        [SerializeField]
        private UnityEvent m_OnActivate = new UnityEvent();

        [SerializeField]
        private UnityEvent m_OnDeactivate = new UnityEvent();

        // Runs once before the first frame.
        void Start() {
            m_Image.transform.SetParent(transform.parent);
            m_TextMesh.text = Key.ToString();
        }

        // Set the keybind group that this is a part of.
        public void SetKeybindGroup(KeybindCollection keybinds) {
            m_KeybindCollection = keybinds;
        }

        // void Start() {
        //     m_Key = GetKeybind();
        //     m_Textbox.SetWord(m_Key.ToString());
        //     m_Textbox.SetSortingLayer(m_SpriteRenderer.sortingLayerName, m_SpriteRenderer.sortingOrder + 5);
        // }

        public KeyCode GetKeybind() {
            if (InputSystem.Instance == null) { return KeyCode.None; }
            KeyCode[] keyCodes = GetArray();
            if (keyCodes == null || m_KeyIndex < 0 || m_KeyIndex >= keyCodes.Length) {
                return KeyCode.None;
            }
            return keyCodes[m_KeyIndex];
        }

        public KeyCode[] GetArray() {
            return m_Type switch {
                KeybindType.Direction => InputSystem.Settings.directionKeybinds,
                KeybindType.Action => InputSystem.Settings.actionKeybinds,
                _ => null
            };
        }

        void Update() {
            if (m_Active) {
                CheckReassignKey();
            }
        }

        public void SetActive(bool active) {
            if (active) {
                DisableAllOthers();
                m_OnActivate.Invoke();
            }
            else {
                m_OnDeactivate.Invoke();
            }
            m_Active = active;
        }

        public void DisableAllOthers() {
            for (int i = 0; i < m_KeybindCollection.Keybinds.Count; i++) {
                if (m_KeybindCollection.Keybinds[i] != this) {
                    m_KeybindCollection.Keybinds[i].SetActive(false);
                }
            }
        }

        public void CheckReassignKey(){

            // ignore frames without keys
            if (UnityEngine.Input.inputString.Length <= 0) { 
                return;
            }

            // Input.inputString stores all keys pressed.
            KeyCode originalKey = Key;
            ReassignKey((KeyCode)UnityEngine.Input.inputString[0]);

            // Check all the other keybinds in case they have the same key.
            for (int i = 0; i < m_KeybindCollection.Keybinds.Count; i++) {
                if (m_KeybindCollection.Keybinds[i] != this && m_KeybindCollection.Keybinds[i].Key == Key && Key != KeyCode.None) {
                    m_KeybindCollection.Keybinds[i].ReassignKey(KeyCode.None);
                }
            }

            // Game.Audio.Sounds.PlaySound(m_OnSetSound);
            SetActive(false);

            // Debug.Log("Reassigned key '" + originalKey + "' to key '" + m_Key + "'");
        
        }

        public void ReassignKey(KeyCode key) {
            KeyCode[] keyCodes = GetArray();
            if (keyCodes == null || m_KeyIndex < 0 || m_KeyIndex >= keyCodes.Length) {
                return;
            }
            keyCodes[m_KeyIndex] = key;
            InputSystem.Settings.Save();
            m_TextMesh.text = Key.ToString();
        }

    }

}

