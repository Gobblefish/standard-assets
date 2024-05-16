// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Input {

    ///<summary>
    /// Processes all the input information for this particular action.
    ///<summary>
    [System.Serializable]
    public class ActionInput {
        
        /* --- Variables --- */
        #region Variables

        // Buffers to allow leeway when inputs are pressed and released.
        public const float PRESS_BUFFER = 0.06f;

        public const float RELEASE_BUFFER = 0.08f;

        [SerializeField] 
        private float m_PressedTicks = 0f;
        
        [SerializeField] 
        private float m_ReleasedTicks = 0f;

        // Swaps the state of this action input.
        [SerializeField] 
        private bool m_Held = false;
        
        [SerializeField] 
        private float m_HeldTicks = 0f;
        
        // The useable information from this action's state.
        public bool Pressed => m_PressedTicks > 0f;
        public bool Held => m_Held;
        public bool Released => m_ReleasedTicks > 0f;
        
        #endregion

        public ActionInput() {
            m_PressedTicks = 0f;
            m_ReleasedTicks = 0f;
            m_Held = false;
            m_HeldTicks = 0f;
        }

        // Updates this action input.
        public void OnUpdate(bool press, bool release, float dt) {
            Swap(ref m_Held, ref m_HeldTicks, press, release, dt);
            Buffer(ref m_PressedTicks, PRESS_BUFFER, press, dt);
            Buffer(ref m_ReleasedTicks, RELEASE_BUFFER, release, dt);
        }

        // Swaps the state of a boolean given two seperate booleans.
        private void Swap(ref bool state, ref float ticks, bool on, bool off, float dt) {
            state = on ? true : off ? false : m_Held;
            ticks = state ? ticks + dt : 0f;
        }

        // Allows for a little buffer time when a input is pressed or released.
        public static void Buffer(ref float ticks, float buffer, bool predicate, float dt) {
            ticks = predicate ? buffer : ticks - dt;
            ticks = ticks < 0f ? 0f : ticks;
        }

        public void ClearPressBuffer() {
            m_PressedTicks = 0f;
        }

        public void ClearReleaseBuffer() {
            m_ReleasedTicks = 0f;
        }

        public void EditHeldDuration(float duration) {
            m_HeldTicks += duration;
            if (m_HeldTicks < 0f) {
                m_HeldTicks = 0f;
            }
        }

    }
}