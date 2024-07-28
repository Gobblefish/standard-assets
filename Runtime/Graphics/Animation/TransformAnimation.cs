// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    [CreateAssetMenu(fileName="TransformAnimation", menuName="Animations/Transform")]
    public class TransformAnimation 
    : Animation<Transform> {
        
        // The parameters for modulating for the target's position.
        [Header("Position")]
        private Vector2 basePosition;
        public Vector2 posScale;
        public AnimationCurve horPosCurve;
        public AnimationCurve vertPosCurve;

        // The parameters for modulating for the target's scale.
        [Header("Stretch")]
        private Vector2 baseStretch;
        public Vector2 strectchScale;
        public AnimationCurve horStretchCurve;
        public AnimationCurve vertStretchCurve;

        // The parameters for modulating for the target's rotation.
        [Header("Rotation")]
        private Quaternion baseRotation;
        public float rotationScale;
        public AnimationCurve rotationCurve;

        // Initializes the base values of the transform.
        protected override void InitializeAnimationTarget() {
            basePosition = target.localPosition; 
            baseRotation = target.localRotation;
            baseStretch = target.localScale;
        }

        // Updates the transform parameters.
        protected override void UpdateAnimationTarget() {
            target.localPosition = GetPosition();
            target.localRotation = GetRotation();
            target.localScale = GetStretch();
        }

        // Resets the current animation target to its default values.
        protected override void ResetAnimationTarget() {
            target.localPosition = basePosition;
            target.localRotation = baseRotation;
            target.localScale = baseStretch;
        }

        // Gets the position at the current ticks.
        private Vector3 GetPosition() {
            return basePosition + new Vector2(
                posScale.x * horPosCurve.Evaluate(t), 
                posScale.y * vertPosCurve.Evaluate(t)
            );
        }

        // Gets the scale/stretch at the current ticks.
        private Vector3 GetStretch() {
            return baseStretch + new Vector2(
                baseStretch.x * strectchScale.x * horStretchCurve.Evaluate(t), 
                baseStretch.y * strectchScale.y * vertStretchCurve.Evaluate(t)
            );
        }

        // Gets the rotation at the current ticks.
        private Quaternion GetRotation() {
            return baseRotation * Quaternion.Euler(
                0f, 
                0f, 
                rotationScale * rotationCurve.Evaluate(t)
            );
        }

    }

}