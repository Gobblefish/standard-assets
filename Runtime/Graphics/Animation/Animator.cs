// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    public class Animator<TAnimation, TAnimationTarget> : MonoBehaviour
    where TAnimation : Animation<TAnimationTarget>
    where TAnimationTarget : Object {

        // The animation that animates through.
        [SerializeField]
        protected new TAnimation animation;

        // Whether this animation is playing or not.
        protected bool animate = true;

        // Controls the playback speed of the animation.
        public float playbackSpeed = 1f;

        // Whether this animation should disable when its done.
        public bool disableOnEnd;

        // Whether this animator should randomize the initial tick.
        public bool randomizeInitialTick;

        // The target of this animation.
        protected TAnimationTarget target;

        // Runs once before the first frame.
        protected virtual void Start() {
            animation = Instantiate(animation);
            animation.Initialize(target);
        }

        public void Play() {
            animate = true;
        }

        public void Pause() {
            animate = false;
        }

        public void Stop() {
            animate = false;
            animation.Reset();
            if (disableOnEnd) {
                Disable();
            }
        }

        protected virtual void FixedUpdate() {
            if (!animate || !animation.hasTarget) { return; }
            
            animation.UpdateAnimation(playbackSpeed * Time.fixedDeltaTime);
            
            if (animation.finished && disableOnEnd) {
                Disable();
                return;
            }

            switch (animation.animEnd) {
                case AnimationEnd.Remove:
                    if (animation.finished)
                        Pause();
                    break;
                case AnimationEnd.Pause:
                    if (animation.finished)
                        Pause();
                    break;
                case AnimationEnd.Loop:
                    if (animation.outsideLoop)
                        animation.Loop();
                    break;
                default:
                    break;
            }

        }

        protected void Disable() {
            gameObject.SetActive(false);
        }

    }

}