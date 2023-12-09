// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Gobblefish.
using Gobblefish.Input.Keyboard;

namespace Gobblefish.Input.Keyboard {

    public class KeybindController : MonoBehaviour {

        // If the controller is actively being assigned.
        [SerializeField]
        private bool m_Active = false;

        // The keycode of this keybind.
        [SerializeField]
        private KeyCode m_Key;
        public KeyCode Key => m_Key;

        [SerializeField]
        private TextMeshProUGUI m_TextMesh;

        // The keybind collection that this is a part of.
        protected KeybindCollection m_KeybindCollection;

        // The image to indicate whether this keybind is active.
        [SerializeField]
        private Image m_Image;

        // The material for the backgrond
        [SerializeField]
        private Material m_ActiveMaterial;

        // The default material for the background.
        private Material m_DefaultMaterial;

        // Runs once before the first frame.
        void Start() {
            m_DefaultMaterial = m_Image.material;
            m_Image.transform.SetParent(transform.parent);
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
            return KeyCode.None;
        }

        void Update() {
            if (m_Active) {
                CheckReassignKey();
            }
        }

        public void SetActive(bool active) {
            if (active) {
                DisableAllOthers();
            }
            m_Active = active;
            m_Image.material = m_Active ? m_ActiveMaterial : m_DefaultMaterial;
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
            KeyCode originalKey = m_Key;
            ReassignKey((KeyCode)UnityEngine.Input.inputString[0]);

            // Check all the other keybinds in case they have the same key.
            for (int i = 0; i < m_KeybindCollection.Keybinds.Count; i++) {
                if (m_KeybindCollection.Keybinds[i] != this && m_KeybindCollection.Keybinds[i].Key == m_Key && m_Key != KeyCode.None) {
                    m_KeybindCollection.Keybinds[i].ReassignKey(KeyCode.None);
                }
            }

            // Game.Audio.Sounds.PlaySound(m_OnSetSound);
            SetActive(false);

            // Debug.Log("Reassigned key '" + originalKey + "' to key '" + m_Key + "'");
        
        }

        public void ReassignKey(KeyCode key) {
            m_Key = key;

            // The guys wan
            m_TextMesh.text = m_Key.ToString();
            // m_Textbox.SetWord(m_Key.ToString());
            // m_Textbox.SetSortingLayer(m_SpriteRenderer.sortingLayerName, m_SpriteRenderer.sortingOrder + 5);

            // Set the new key.
            // if (m_InputMovementIndex < Game.Input.Settings.movement.Length) {
            //     Game.Input.Settings.movement[m_InputMovementIndex] = m_Key;
            // }

        }

    }

}

