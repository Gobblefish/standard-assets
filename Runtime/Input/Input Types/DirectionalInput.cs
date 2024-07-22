// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Input {

    ///<summary>
    /// Processes all the directional input information.
    ///<summary>
    public class DirectionalInput {

        // The actual inputted direction,
        // Whether this is a joystick, keypad or anything else.
        [SerializeField] 
        private Vector2 m_Vector = new Vector2(0f, 0f);

        // The normalized direction.
        public Vector2 Normal => m_Vector.normalized;

        // The horizontal direction.
        public float Horizontal => m_Vector.x;

        // The vertical direction.
        public float Vertical => m_Vector.y;

        // The last pressed input.
        private Vector2 m_MostRecent = new Vector2(0f, 0f);
        public Vector2 MostRecent => m_MostRecent.normalized;

        // Whether this is zero.
        public bool Inactive => m_Vector == Vector2.zero;

        // Updates this directional input.
        public void OnUpdate(Vector2 vector) {
            bool newX = vector.x != 0f && vector.x != m_Vector.x;
            bool newY = vector.y != 0f && vector.y != m_Vector.y;
            if (newX) {
                m_MostRecent = new Vector2(vector.x, 0f);
            }
            else if (newY) {
                m_MostRecent = new Vector2(0f, vector.y);
            }
            
            m_Vector = vector;
        }

        public void Clear() {
            m_Vector = new Vector2(0f, 0f);
            m_MostRecent = new Vector2(0f, 0f);
        }

    }

}