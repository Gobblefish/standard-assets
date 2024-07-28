// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    public enum AnimationEnd {
        Pause,
        Remove,
        Loop
    }

    public abstract class Animation<TAnimationTarget> : ScriptableObject
    where TAnimationTarget : Object {

        // The duration the animation lasts for.
        public float duration = 1f;

        // What to do when the animation ends.
        public AnimationEnd animEnd;

        // The current tick position of the animation.
        protected float ticks = 0f;
        protected float t => ticks / duration;
        public bool finished => t >= 1f;
        public bool outsideLoop => t < 0f || t >= 1f;

        // The actual animation target.
        protected TAnimationTarget target;
        public bool hasTarget => target != null;

        // Initializes the animation with a given target.      
        public void Initialize(TAnimationTarget target) {
            InitializeTick();
            SetAnimationTarget(target);
        }

        // Updates the animation.
        public void UpdateAnimation(float dt) {
            UpdateTick(dt);
            UpdateAnimationTarget();
        }

        // What this animation does when it ends.
        public void Loop() {
            LoopTick();
        }

        // What this animation does when it ends.
        public void Reset() {
            ResetAnimationTarget();
        }

        // Sets the target for animation.
        public void SetAnimationTarget(TAnimationTarget target) {
            this.target = target;
            InitializeAnimationTarget();
        }

        // Initializes the animation target.
        protected abstract void InitializeAnimationTarget();

        // What the animation target does while it is playing. 
        protected abstract void UpdateAnimationTarget();

        // Resets the current animation target to its default values.
        protected abstract void ResetAnimationTarget();

        // Initializes the ticks.
        private void InitializeTick(bool random = false) {
            if (random) {
                ticks = Random.Range(0f, duration);
            }
            else {
                ticks = 0f;
            }
        }

        // Updates the tick given the delta time.
        private void UpdateTick(float dt) {
            ticks += dt;
        }

        // Sets the time for the animation.
        public void SetToTick(float newTick) { // TODO: delete.
            ticks = newTick;
            UpdateAnimationTarget(); // unsure about this.
        }

        // Sets the duration for the animation.
        public void SetDuration(float newDuration) { // TODO: delete.
            duration = newDuration; // but duration is public so whereever i set it from
        }

        // Ticks the animation.
        private void LoopTick() {
            int loopCount = GetLoopCount(ticks, duration);

            // If the ticks have gone below zero.
            if (ticks < 0f) {
                ticks += (loopCount * duration);
            }
            // If the ticks have gone past the duration.
            else if (ticks >= duration) {
                ticks -= (loopCount * duration);
            }

        }

        private static int GetLoopCount(float ticks, float duration) {
            return (int)Mathf.Ceil(Mathf.Abs(ticks) / duration); // Accounts for how many times it might be below the duration. 
        }

    }

}