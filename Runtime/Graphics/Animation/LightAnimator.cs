/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gobblefish.Graphics {

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class LightAnimation {
        
        // The parameters of the animation.
        [Header("Params")]
        private float ticks;
        public float duration;
        private float t => ticks / duration;

        // How the intensity varies.
        [Header("Intensity")]
        private float baseIntensity;
        public float intensityFluxScale;
        public AnimationCurve intensityCurve;
        
        // The inner radius of the light.
        [Header("Inner Radius")]
        private float baseInnerRadius;
        public float innerRadiusFluxScale;
        public AnimationCurve innerRadiusCurve;

        // The outer radius of the light.
        [Header("Outer Radius")]
        private float baseOuterRadius;
        public float outerRadiusFluxScale;
        public AnimationCurve outerRadiusCurve;

        // Set the values from the lights.
        public void SetLightParams(Light2D light) {
            baseIntensity = light.intensity;
            baseInnerRadius = light.pointLightInnerRadius ;
            baseOuterRadius = light.pointLightOuterRadius ;
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

        public float GetIntensity() {
            return baseIntensity + intensityFluxScale * intensityCurve.Evaluate(t);
        }
        
        public float GetInnerRadius() {
            return baseInnerRadius + innerRadiusFluxScale * innerRadiusCurve.Evaluate(t);
        }

        public float GetOuterRadius() {
            return baseOuterRadius + outerRadiusFluxScale * outerRadiusCurve.Evaluate(t);
        }

    }

    ///<summary>
    ///
    ///<summary>
    [RequireComponent(typeof(Light2D))]
    public class LightAnimator : MonoBehaviour {

        // The idle light animation.
        [SerializeField]
        private LightAnimation m_Animation;

        [SerializeField]
        private bool m_Animate;

        // The light attacted to this.
        private Light2D m_Light;

        // Runs once on instantiation.
        void Start() {
            m_Light = GetComponent<Light2D>();
            m_Animation.SetLightParams(m_Light);
        }

        void FixedUpdate() {
            if (!m_Animate) { return; }
            Animate(Time.fixedDeltaTime);
        }

        public void Animate(float dt) {
            m_Animation.Tick(dt);
            m_Light.intensity = m_Animation.GetIntensity();
            m_Light.pointLightInnerRadius = m_Animation.GetInnerRadius(); 
            m_Light.pointLightOuterRadius = m_Animation.GetOuterRadius();
        }

        public static void Animate(Light2D light, LightAnimation animation, float dt) {
            animation.Tick(dt);
            light.intensity = animation.GetIntensity();
            light.pointLightInnerRadius = animation.GetInnerRadius(); 
            light.pointLightOuterRadius = animation.GetOuterRadius();
        }

    }

}
