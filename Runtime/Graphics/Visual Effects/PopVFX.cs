// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.VisualEffects {

    public class PopVFX : VFX {

        [System.Serializable]
        public class Params {
            public float duration;

            public int radius;
            public float speed;
            public ParticleSystemShapeType shapeType;
            public int delay;
            public int particleCount;
            public Vector3 position;
            public Gradient colorOverLifetime;

            public void SetPositionToTransform(Transform transform) {
                position = transform.position;
            }

            public void SetPositionToSplinePercent() {

            }

        }

        public float duration;
        public int radius;
        // public Vector2 speedRange;
        public float speed;
        public ParticleSystemShapeType shapeType;
        public float delay;
        public int count;
        public Gradient colorOverLifetime;

        private ParticleSystem.Burst[] bursts;

        public static void Play(Params popParams) {

            GameObject newObject = new GameObject("", typeof(PopVFX));
            PopVFX popVFX = newObject.GetComponent<PopVFX>();
            
            popVFX.radius = popParams.radius;
            popVFX.speed = popParams.speed;
            popVFX.shapeType = popParams.shapeType;
            popVFX.count = popParams.particleCount;
            popVFX.duration = popParams.duration;

            popVFX.transform.position = popParams.position;
            
            popVFX.Stop();
            popVFX.Create();
            popVFX.Play(true);

        } 

        public float minMax = 1.5f;

        public override void Create() {
            if (Application.isPlaying) { Destroy(gameObject, duration); }

            // The main parameters.
            main.startSpeed = speed;
            main.duration = duration;
            main.loop = false;


            // The emission parameters.
            bursts = new ParticleSystem.Burst[] { new ParticleSystem.Burst(delay, count) };
            em.SetBursts(bursts);

            // The shape parameters.
            shape.shapeType = shapeType;
            shape.radius = radius;

            // Size over lifetime.
            sz.enabled = true;

            AnimationCurve curve = new AnimationCurve();
            // curve.ClearKeys();
            curve.AddKey(0.0f, 1.0f);
            curve.AddKey(0.75f, 1.0f);
            curve.AddKey(1f, 0f);
            
            sz.size = new ParticleSystem.MinMaxCurve(minMax, curve);

        }


    }

}
