/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Gobblefish.Graphics {

    ///<summary>
    /// Controls the position and quality of the camera.
    ///<summary>
    [RequireComponent(typeof(Camera))]
    public class CameraShake : MonoBehaviour {

        // The different states.
        public enum ShakeState {
            Stable,
            Shaking
        }

        // The current shake state of this camera.
        private ShakeState m_ShakeState = ShakeState.Stable;

        // The timer for the camera shake.
        private float m_Ticks = 0f;
        private float m_Duration = 1f;

        // The animation curve for shaking the screen.
        [SerializeField]
        private AnimationCurve m_ShakeCurve;

        // The amount of strength the current screen shake has.
        [HideInInspector]
        private float m_ShakeStrength = 1f;

        // Runs once per frame.
        void Update() {
            // What to do for each state.
            switch (m_ShakeState) {
                case ShakeState.Shaking:
                    WhileShaking();
                    break;
                default:
                    break;
            }
        }

        // Runs once every fixed interval.
        void FixedUpdate() {
            m_Ticks -= Time.fixedDeltaTime;
            if (m_Ticks <= 0f && m_ShakeState != ShakeState.Stable) {
                m_ShakeState = ShakeState.Stable;
            }
        }

        // Starts the camera shaking.
        public void ShakeCamera(float strength, float duration) {
            m_ShakeStrength = Mathf.Max(m_ShakeStrength, strength);
            m_Ticks = duration;
            m_Duration = duration;
            m_ShakeState = ShakeState.Shaking;
        }

        // The way the camera moves while it is shaking.
        public void WhileShaking() {
            float strength = GraphicsManager.Settings.camShake * m_ShakeStrength * m_ShakeCurve.Evaluate(1f - (m_Ticks / m_Duration));
            transform.localPosition = strength * (Vector3)Random.insideUnitCircle.normalized;
        }

        // Gets a random position within the screen bounds.
        public Vector2 RandomPositionWithinBounds() {
            return Vector2.zero;
        }

        public bool IsWithinBounds(Vector2 position) {
            return true;
        }

    }

}