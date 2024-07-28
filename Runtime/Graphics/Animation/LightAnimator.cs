// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GobbleFish.Animations {

    public class LightAnimator : Animator<LightAnimation, Light2D> {
        
        // Runs once before the first frame.
        protected override void Start() {
            target = GetComponent<Light2D>();
            base.Start();
        }

    }

}