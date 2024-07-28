// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    public class ChildrenAnimator : Animator<TransformAnimation, Transform> {
        
        // To hold all the animations.
        protected List<TransformAnimation> animations = new List<TransformAnimation>();

        // The number of animations being itterated through.
        protected int animationCount = 0;

        // Runs once before the first frame.
        protected override void Start() {
            animations = new List<TransformAnimation>();

            foreach (Transform child in transform) {
                target = child;
                base.Start();

                animations.Add(animation);
            }

            animationCount = animations.Count;
            
        }

        protected override void FixedUpdate() {
            if (!animate) { return; }
            
            for (int i = 0; i < animationCount; i++) {
                animations[i].UpdateAnimation(Time.fixedDeltaTime);
            }
        }

    }

}