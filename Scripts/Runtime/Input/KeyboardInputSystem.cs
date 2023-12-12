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

        #region Variables.

        // Directional Input Keybinds.
        [SerializeField] private float HorizontalDirection => UnityEngine.Input.GetAxisRaw("Horizontal");
        [SerializeField] private float VerticalDirection => UnityEngine.Input.GetAxisRaw("Vertical");

        // Action Input Keybinds.
        [SerializeField] private KeyCode m_ActionKeybind0 = KeyCode.Space;
        public bool PressAction0 => UnityEngine.Input.GetKeyDown(m_ActionKeybind0);
        public bool ReleaseAction0 => UnityEngine.Input.GetKeyUp(m_ActionKeybind0);

        [SerializeField] private KeyCode m_ActionKeybind1 = KeyCode.J;
        public bool PressAction1 => UnityEngine.Input.GetKeyDown(m_ActionKeybind1);
        public bool ReleaseAction1 => UnityEngine.Input.GetKeyUp(m_ActionKeybind1);

        [SerializeField] private KeyCode m_ActionKeybind2 = KeyCode.K;
        public bool PressAction2 => UnityEngine.Input.GetKeyDown(m_ActionKeybind2);
        public bool ReleaseAction2 => UnityEngine.Input.GetKeyUp(m_ActionKeybind2);

        #endregion

        // Updates the inputs.
        protected override void Think(float dt) {
            // Updates the directional input.
            m_Direction.OnUpdate(new Vector2(HorizontalDirection, VerticalDirection));

            // Updates each of the action buttons.
            m_Action0.OnUpdate(PressAction0, ReleaseAction0, dt);
            m_Action1.OnUpdate(PressAction1, ReleaseAction1, dt);
            m_Action2.OnUpdate(PressAction2, ReleaseAction2, dt);
        }

    }

}