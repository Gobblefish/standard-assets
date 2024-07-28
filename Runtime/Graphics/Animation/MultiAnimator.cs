// // System.
// using System.Collections;
// using System.Collections.Generic;
// // Unity.
// using UnityEngine;

// namespace GobbleFish.Animations {

//     public class MultiAnimator<TAnimation, TAnimationTarget> : MonoBehaviour
//     where TAnimation : Animation<TAnimationTarget>
//     where TAnimationTarget : Object {

//         // The list of things to animate.
//         [SerializeField]
//         protected TAnimationTarget[] animationTargets;
        
//         // To hold all the animations.
//         protected TAnimation animation;

//         // The number of animations being itterated through.
//         protected int animationCount = 0;

//         // Runs once before the first frame.
//         protected override void Start() {
//             animations = new List<TransformAnimation>();

//             foreach (Transform child in transform) {
//                 target = child;
//                 base.Start();

//                 animations.Add(animation);
//             }

//             animationCount = animations.Count;
            
//         }

//         protected override void FixedUpdate() {
//             if (!animate) { return; }
            
//             for (int i = 0; i < animationCount; i++) {
//                 animations[i].UpdateAnimation(Time.fixedDeltaTime);
//             }
//         }

//     }

// }