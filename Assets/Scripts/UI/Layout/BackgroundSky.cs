// // System.
// using System.Collections;
// using System.Collections.Generic;
// // Unity.
// using UnityEngine;
// //
// using Gobblefish.Visuals;
// using Gobblefish.Visuals.Transforms;

// namespace Gobblefish.Visuals {

// public class BackgroundSky : MonoBehaviour {

//     public SpriteRenderer currentSky;
//     public SpriteRenderer cachedSky;

//     public float height = 20f;

//     public bool finishedTransitioning = false;
//     public float ticks = 0f;
//     public float duration = 0.7f;

//     public void TransitionTo(SpriteRenderer spriteRenderer) {
//         if (spriteRenderer == null) {
//             return;
//         }

//         spriteRenderer.transform.SetParent(null);

//         if (true || currentSky == spriteRenderer || currentSky == null) {
//             currentSky = spriteRenderer;
//             ticks = duration;
//             finishedTransitioning = true;

//             currentSky.transform.localScale = new Vector3(1f, 1f, 1f);
//             currentSky.transform.position = Vector3.zero;

//         }
//         else {
//             cachedSky = spriteRenderer;
//             ticks = 0f;
//             finishedTransitioning = false;
//         }

//     }

//     void FixedUpdate() {
//         if (finishedTransitioning) {
//             return;
//         }

//         float dt = Time.fixedDeltaTime;
        
//         ticks += dt;
//         if (ticks >= duration) {
//             currentSky = cachedSky;
//             cachedSky = null;
//             ticks = duration;
//             finishedTransitioning = true;

//             if (currentSky != null) {
//                 currentSky.transform.localScale = new Vector3(1f, 1f, 1f);
//                 currentSky.transform.position = Vector3.zero;
//             }

//             return;
//         }

//         Vector3 v = new Vector3(1f, ticks / duration, 1f);

//         float offset = -height * (1f- v.y) / 2f; 
//         Vector3 p = new Vector3(0f, offset, 0f);
//         if (cachedSky != null) {
//             cachedSky.transform.localScale = v;
//             cachedSky.transform.position = p;
//         }

//         v.y = (1f - v.y);
//         offset = height * (1f - v.y) / 2f;
//         p.y = offset;
//         if (currentSky != null) {
//             currentSky.transform.localScale = v;
//             currentSky.transform.position = p;
//         }

//     }

// }

// }