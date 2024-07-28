// // System.
// using System.Collections;
// using System.Collections.Generic;
// // Unity.
// using UnityEngine;

// namespace GobbleFish.Animations {

//     /// <summary>
//     /// Attach this to an animator to dynamically affect the playback of the animation.
//     /// Useful for dynamic slow-mo or speed up effects.
//     /// </summary>
//     public class AnimatorPlaybackModulator : MonoBehaviour {

//         // The curve that affects the animation.
//         public AnimationCurve modCurve;

//         // The amplitude of the modulation.
//         public float modAmp;

//         // Whether the modulation is active.
//         public bool active;

//         public float durationScale; // => an int that describes how
//         // many times into the animator's period it goes
//         // e.g. 2 means it cycles twice through one period of the actual animation 
//         float ticks;
//         float t => ticks / durationScale;

//         public Animator animator;

//         // The playback speed of the animator.
//         private float basePlaybackSpeed;

//         // Runs once before the first frame.
//         void Start() { }

//         // Runs once every fixed interval.
//         void FixedUpdate() {
//             if (active) {
//                 UpdateTicks();
//                 ModulatePlayback();
//             }
//         }

//         private void SetAnimatorParameters() {
//             basePlaybackSpeed = animator.playbackSpeed;

//             // duration = animator.animation.duration;
//             // dt = playbackSpeed(t) * fdt / duration;
//             // t_total = integrate ( dt )^t1_t0;
//             // 
            
//         }

//         private void GetModulatedDuration() {
//             float area = IntegrateCurve();


//         }

//         private void UpdateTicks() {
//             ticks += Time.fixedDeltaTime;
//             if (ticks > duration) {
//                 ticks -= duration;
//             }
//         }

//         protected void ModulatePlayback() {
//             float a = modAmp;
//             float m = basePlaybackSpeed;
//             float x = modCurve.Evaluate(t);
//             animator.playbackSpeed = a * m * x + (1-a) * m;
//         }

//     }

// }