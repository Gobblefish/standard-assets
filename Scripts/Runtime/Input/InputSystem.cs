// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Input {

    ///<summary>
    /// Collects the inputs for a character.
    ///<summary>
    public abstract class InputSystem : Gobblefish.Manager<InputSystem, InputSettings> {

        // The characters directional input.
        [SerializeField] 
        protected DirectionalInput m_Direction = new DirectionalInput();
        public DirectionalInput Direction => m_Direction;

        // The characters action inputs.
        [SerializeField] 
        protected ActionInput[] m_Actions;
        public ActionInput[] Actions => m_Actions;

        // Runs once every frame.
        void Update() {
            Think(Time.deltaTime);
        }

        // Updates the inputs.
        protected virtual void Think(float dt) {
            // Updates the directional input.
            m_Direction.OnUpdate(Vector2.zero);

            // Updates each of the action buttons.
            for (int i = 0; i < m_Actions.Length; i++) {
                m_Actions[i].OnUpdate(false, false, dt);
            }

        }

    }

}
