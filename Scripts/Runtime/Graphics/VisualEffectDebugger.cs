/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Experimental.Rendering.Universal;

namespace Gobblefish.Graphics {

    ///<summary>
    /// 
    ///<summary>
    [ExecuteInEditMode]
    public class VisualEffectDebugger : MonoBehaviour {

        // The effects under this transform.
        [SerializeField]
        private List<VisualEffect> m_VisualEffects = new List<VisualEffect>();

        // The timer for the camera shake.
        [SerializeField]
        private float m_Ticks = 0f;
        public float m_Duration = 1f;

        // Runs once every fixed interval.
        void Update() {
            m_VisualEffects = new List<VisualEffect>();
            foreach (Transform child in transform) {
                VisualEffect vfx = child.GetComponent<VisualEffect>();
                if (vfx != null) {
                    m_VisualEffects.Add(vfx);
                }
            }

            m_Ticks -= Time.deltaTime;
            if (m_Ticks <= 0f) {
                for (int i = 0; i < m_VisualEffects.Count; i++) {
                    m_VisualEffects[i].Play();
                }
                m_Ticks = m_Duration;
            }
        }

    }

}