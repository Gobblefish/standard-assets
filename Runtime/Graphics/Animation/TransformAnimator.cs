// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    public class TransformAnimator : Animator<TransformAnimation, Transform> {
        
        // Runs once before the first frame.
        protected override void Start() {
            target = transform;
            base.Start();
        }

    }

}