// using UnityEngine;
// using System.Collections;
// using Gobblefish.Graphics;

// namespace Gobblefish.Graphics {

//     public class PaintTrail : MonoBehaviour {

//         LineRenderer l;

//         public bool useCurve;

//         public float width = 1f;

//         Vector3[] r;

//         public int segmentCount = 100;

//         void Awake () {
            
//             l = GetComponent<LineRenderer>();

//             r = new Vector3[segmentCount];
//             for (int i = 0; i < segmentCount; i++) {
//                 r[i] = Vector3.zero;// * (float)i / ((float)segmentCount / length);
//             }

//             l.positionCount = segmentCount;
//             l.SetPositions(r);

//         }

//         float ticks = 0f;
//         float _ticks = 0f;

//         public float m_PositionWavelength = 1f;
//         public float m_TimeWavelength = 1f;
//         public float m_MinimumThickness = 0.2f;

//         public float rl_w = 1f;
//         public float rl_t = 1f;

//         public float length = 3f;

//         public float amp;

//         public float duration;

//         public float speed;

//         public Transform track;
        
//         void FixedUpdate () {

//             ticks += Time.fixedDeltaTime;
//             _ticks += Time.fixedDeltaTime;

//             if (_ticks < duration) {
//                 return;
//             }

//             _ticks -= duration;

//             AnimationCurve curve = new AnimationCurve();
//             for (int i = 1; i < r.Length; i++) {

//                 float w = (float)i / (float)r.Length;

//                 float thickness = (1f-m_MinimumThickness) * Mathf.Pow(Mathf.Sin(m_PositionWavelength * w + m_TimeWavelength * ticks), 2) + m_MinimumThickness;
//                 thickness *= Mathf.Sqrt(w);
//                 curve.AddKey(w, thickness);
//                 // r[i] = Vector3.right * (float)i / ((float)r.Length / length) + amp * Vector3.up * Mathf.Sin(rl_w * w + rl_t * ticks);
//                 r[i-1] = r[i];
                
//             }

//             r[r.Length - 1] = track.position + amp * Vector3.up * Mathf.Sin(rl_w * (r.Length - 1) + rl_t * ticks);

//             track.position += Vector3.right * speed;

//             l.widthCurve = curve;
//             l.widthMultiplier = width;
//             l.SetPositions(r);

//         }

//     }

// }
