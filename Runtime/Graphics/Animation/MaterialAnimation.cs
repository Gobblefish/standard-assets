// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    [CreateAssetMenu(fileName="MaterialAnimation", menuName="Animations/Material")]
    public class MaterialAnimation : Animation<Material> {
        
        // How the intensity varies.
        [Header("Variable")]
        private float baseVal;
        public string variableName;
        public float fluxScale;
        public AnimationCurve fluxCurve;

        // Initializes the base values of the transform.
        protected override void InitializeAnimationTarget() { 
            baseVal = target.GetFloat(variableName);
        }

        // Updates the transform parameters.
        protected override void UpdateAnimationTarget() {
            target.SetFloat(variableName, GetValue());
        }

        protected override void ResetAnimationTarget() {
            target.SetFloat(variableName, baseVal);
        }

        // Gets the value of the variable at the current ticks.
        public float GetValue() {
            return baseVal + fluxScale * fluxCurve.Evaluate(t);
        }

    }

}
