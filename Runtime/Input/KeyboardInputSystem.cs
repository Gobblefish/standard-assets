// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Input {

    ///<summary>
    /// Collects the inputs from a keyboard.
    ///<summary>
    public class KeyboardInputSystem : InputSystem {

        // Directional Input Keybinds.
        // [SerializeField] 
        // private float HorizontalDirection => UnityEngine.Input.GetAxisRaw("Horizontal");
        
        // [SerializeField] 
        // private float VerticalDirection => UnityEngine.Input.GetAxisRaw("Vertical");

        // The characters direction action inputs.
        [SerializeField] 
        protected ActionInput[] m_DirectionActions;

        // Action Input Keybinds. 
        // [SerializeField]
        // private KeyCode[] m_ActionKeybinds; 

        protected override void CreateInputs() {
            base.CreateInputs();
            m_DirectionActions = new ActionInput[Settings.directionKeybinds.Length];
            for (int i = 0; i < m_DirectionActions.Length; i++) {
                m_DirectionActions[i] = new ActionInput();
            }
        }

        // Updates the inputs.
        protected override void Think(float dt) {
            
            // Updates each of the action buttons.
            for (int i = 0; i < m_DirectionActions.Length; i++) {
                if (Settings.directionKeybinds.Length > i) {
                    m_DirectionActions[i].OnUpdate(Pressed(Settings.directionKeybinds[i]), Released(Settings.directionKeybinds[i]), dt);
                }
                else {
                    m_DirectionActions[i].OnUpdate(false, false, dt);
                }
            }

            float verticalDir = m_DirectionActions[0].Held.ToInt() - m_DirectionActions[2].Held.ToInt();
            float horizontalDir = -m_DirectionActions[1].Held.ToInt() + m_DirectionActions[3].Held.ToInt();

            // Updates the directional input.
            m_Direction.OnUpdate(new Vector2(horizontalDir, verticalDir));

            // Updates each of the action buttons.
            for (int i = 0; i < m_Actions.Length; i++) {
                if (Settings.actionKeybinds.Length > i) {
                    m_Actions[i].OnUpdate(Pressed(Settings.actionKeybinds[i]), Released(Settings.actionKeybinds[i]), dt);
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

        // public void SetDirectionKeybind(int directionIndex, KeyCode keyCode) {
        //     if (directionIndex >= 0 && directionIndex < Settings.directionKeybinds.Length) {
        //         Settings.directionKeybinds[directionIndex] = keyCode;
        //     }
        // }

        // public void SetActionKeybind(int actionIndex, KeyCode keyCode) {
        //     if (actionIndex >= 0 && actionIndex < Settings.actionKeybinds.Length) {
        //         Settings.actionKeybinds[actionIndex] = keyCode;
        //     }
        // }

    }

}