// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GobbleFish.Animations {

    [CreateAssetMenu(fileName="LightAnimation", menuName="Animations/Light")]
    public class LightAnimation : Animation<Light2D> {
        
        // The parameters for modulating for the target's position.
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

        // Initializes the base values of the transform.
        protected override void InitializeAnimationTarget() { 
            baseIntensity = target.intensity;
            baseInnerRadius = target.pointLightInnerRadius ;
            baseOuterRadius = target.pointLightOuterRadius ;
        }

        // Updates the transform parameters.
        protected override void UpdateAnimationTarget() {
            target.intensity = GetIntensity();
            target.pointLightInnerRadius = GetInnerRadius(); 
            target.pointLightOuterRadius = GetOuterRadius();
        }

        protected override void ResetAnimationTarget() {
            target.intensity = baseIntensity;
            target.pointLightInnerRadius = baseInnerRadius; 
            target.pointLightOuterRadius = baseOuterRadius;
        }

        // Gets the intensity at the current ticks.
        public float GetIntensity() {
            return baseIntensity + 
                (intensityFluxScale * intensityCurve.Evaluate(t));
        }
        
        // Gets the inner radius at the current ticks.
        public float GetInnerRadius() {
            return baseInnerRadius + 
                (innerRadiusFluxScale * innerRadiusCurve.Evaluate(t));
        }

        // Gets the outer radius at the current ticks.
        public float GetOuterRadius() {
            return baseOuterRadius + 
                (outerRadiusFluxScale * outerRadiusCurve.Evaluate(t));
        }

    }

}
