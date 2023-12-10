/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gobblefish.Animation {

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class MaterialFloatAnimation {

        // Just to see how well this updates.
        
        // The parameters of the animation.
        [Header("Params")]
        private float ticks;
        public float duration;
        private float t => ticks / duration;

        // How the intensity varies.
        [Header("VariableName")]
        public string varName;
        
        private float baseVal;
        public float fluxScale;
        public AnimationCurve fluxCurve;

        // Set the values from the lights.
        public void GetValueParams(Material mat) {
            baseVal = mat.GetFloat(varName);
        }

        // Set the time of the animation.
        public void SetTime(float t) {
            ticks = t;
            if (ticks > duration) {
                ticks = ticks % duration;
            }
        }

        // Tick the animation.
        public void Tick(float dt) {
            ticks += Time.fixedDeltaTime;
            if (ticks > duration) {
                ticks -= duration;
            }
        }

        public float GetValue() {
            return baseVal + fluxScale * fluxCurve.Evaluate(t);
        }

    }

    ///<summary>
    ///
    ///<summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class MaterialAnimator : MonoBehaviour {

        // The idle light animation.
        [SerializeField]
        private MaterialFloatAnimation[] m_FloatAnimations;

        [SerializeField]
        private bool m_Animate;

        // The light attacted to this.
        private SpriteRenderer m_SpriteRenderer;

        // Runs once on instantiation.
        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            for (int i = 0; i < m_FloatAnimations.Length; i++) {
                m_FloatAnimations[i].GetValueParams(m_SpriteRenderer.material);
            } 
        }

        void FixedUpdate() {
            if (!m_Animate) { return; }
            Animate(Time.fixedDeltaTime);
        }

        public void Animate(float dt) {
            for (int i = 0; i < m_FloatAnimations.Length; i++) {
                m_FloatAnimations[i].Tick(dt);
                m_SpriteRenderer.material.SetFloat(m_FloatAnimations[i].varName, m_FloatAnimations[i].GetValue());
            } 
        }

        public static void Animate(Light2D light, LightAnimation animation, float dt) {
            animation.Tick(dt);
            //
        }

    }

}
