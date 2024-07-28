// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    public class SpriteAnimator : Animator<SpriteAnimation, SpriteRenderer> {
        
        // Runs once before the first frame.
        protected override void Start() {
            target = GetComponent<SpriteRenderer>();
            base.Start();
        }

    }

}