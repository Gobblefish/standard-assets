// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Input;

namespace Gobblefish.Input {

    ///<summary>
    /// Collects the inputs from a keyboard.
    ///<summary>
    public class KeyboardInputSystem : InputSystem {

        // Directional Input Keybinds.
        [SerializeField] private float HorizontalDirection => UnityEngine.Input.GetAxisRaw("Horizontal");
        [SerializeField] private float VerticalDirection => UnityEngine.Input.GetAxisRaw("Vertical");

        // Action Input Keybinds.
        [SerializeField] 
        private KeyCode[] m_ActionKeybinds;

        // Updates the inputs.
        protected override void Think(float dt) {
            // Updates the directional input.
            m_Direction.OnUpdate(new Vector2(HorizontalDirection, VerticalDirection));

            // Updates each of the action buttons.
            for (int i = 0; i < m_Actions.Length; i++) {
                if (m_ActionKeybinds.Length > i) {
                    m_Actions[i].OnUpdate(Pressed(m_ActionKeybinds[i]), Released(m_ActionKeybinds[i]), dt);
                }
                else {
                    m_Actions[i].OnUpdate(false, false, dt);
                }
            }

        }

        private bool Pressed(KeyCode keyCode) {
            return UnityEngine.Input.GetKeyDown(keyCode);
        }

        private bool Released(KeyCode keyCode) {
            return UnityEngine.Input.GetKeyUp(keyCode);
        }

    }

}