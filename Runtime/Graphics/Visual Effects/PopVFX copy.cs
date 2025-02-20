// // System.
// using System.Collections;
// using System.Collections.Generic;
// // Unity.
// using UnityEngine;

// namespace GobbleFish {

//     public class TM_FILENAME_BASE : VFX {

//         public int radius;
//         // public Vector2 speedRange;
//         public float speed;
//         public ParticleSystemShapeType shapeType;
//         public float delay;
//         public float count;

//         private ParticleSystem.Burst[] bursts;

//         public override void Create() {

//             // The main parameters.
//             main.startSpeed = speed;

//             // The emission parameters.
//             bursts = new ParticleSystem.Burst[] { new ParticleSystem.Burst(delay, count) };
//             em.SetBursts(bursts);

//             // The shape parameters.
//             shape.shapeType = shapeType;
//             shape.radius = radius;

//         }


//     }

// }
