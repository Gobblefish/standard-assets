// // System.
// using System.Collections;
// using System.Collections.Generic;
// // Unity.
// using UnityEngine;

// namespace GobbleFish.Animations {

//     using UnityAnimator = UnityEngine.Animator;
//     using UnityAnimationClip = UnityEngine.AnimationClip;

//     [CreateAssetMenu(fileName="UnityAnimation", menuName="Animations/Unity")]
//     public class UnityAnimation : Animation<GameObject> {

//         public UnityAnimationClip animation;

//         public Vector2 playbackRange = new Vector2(0f, 1f);

//         public float minTicks => animation == null ? 0f : playbackRange.x * animation.length;
//         public float maxTicks => animation == null ? 0f : playbackRange.y * animation.length;

//         // Initializes the base values of the transform.
//         protected override void InitializeAnimationTarget() { }

//         // Updates the transform parameters.
//         protected override void UpdateAnimationTarget() {
            
//             if (ticks >= anim.maxTicks) {
//                 if (anim.loop) {
//                     ticks -= anim.maxTicks;
//                 }
//                 else {
//                     if (anim.removeOnEnd) {
//                         StopAnimation(anim.name);
//                         return;
//                     }
//                     ticks = anim.maxTicks;
//                 }
//             }

//             anim.animation.SampleAnimation(gameObject, ticks);
//         }

//         protected virtual float GetScaledTime() {
//             return ticks / duration;
//         }

//         public float duration => GetStartTime() - GetEndTime();

//         protected virtual float GetStartTime() {
//             return playbackRange.x * animation.length;
//         }

//         protected virtual float GetEndTime() {
//             return playbackRange.y * animation.length;
//         } 

//     }

// }
