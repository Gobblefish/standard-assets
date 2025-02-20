// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.VisualEffects {

    [ExecuteInEditMode, RequireComponent(typeof(ParticleSystem))]
    public abstract class VFX : MonoBehaviour {

        protected new ParticleSystem particleSystem;

        protected ParticleSystem.MainModule main;
        protected ParticleSystem.EmissionModule em;
        protected ParticleSystem.ShapeModule shape;
        protected ParticleSystem.SizeOverLifetimeModule sz;


        // Controls.
        public bool play = false;
        public bool recreate = false;

        public void OnEnable() {
            particleSystem = this.Require<ParticleSystem>();
            
            main = particleSystem.main;
            em = particleSystem.emission;
            shape = particleSystem.shape;
            sz = particleSystem.sizeOverLifetime;

            MakeParticleSystemBlank();
            // if (!Application.isPlaying) {
            //     Create();
            // }
        }
        
        void Update() {
            if (!Application.isPlaying) {
                Play(play);
                if (recreate) {
                    Stop();
                    Create();
                    recreate = false;
                    Play(play);
                }
            }
        }

        public void MakeParticleSystemBlank() {
            em.rateOverTime = 0;
        }

        public void Play(bool play) {
            if (play) {
                particleSystem.Play();
            }
            else {
                particleSystem.Pause();
            }
        }

        public void Stop() {
            particleSystem.Stop();
        }

        public abstract void Create();

    }

}
