// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    public class StateAnimator<TCollection, TAnimation, TAnimationTarget> 
    : MonoBehaviour
    where TCollection : ScriptableCollection<TAnimation>
    where TAnimation : Animation<TAnimationTarget>
    where TAnimationTarget : Object {

        // An enumeration for the different animation priorities.
        public enum Priority {
            Default,
            Low, Low0, Low1, Low2,
            Medium, Medium0, Medium1, Medium2,
            High, High0, High1, High2,
            VeryHigh, VeryHigh0, VeryHigh1, VeryHigh2,
            Count
        }

        // The animation that animates through.
        [SerializeField]
        protected TCollection animations;
        protected TAnimation[] animationSheet;

        // Whether this animation is playing or not.
        protected bool animate = true;

        // Controls the playback speed of the animation.
        public float playbackSpeed = 1f;

        // The target of this animation.
        protected TAnimationTarget target;

        // Runs once before the first frame.
        protected virtual void Start() {
            animations = Instantiate(animations);
            foreach (ScriptableCollection<TAnimation>.ItemWrapper wrapper in animations.collection) {
                wrapper.item.Initialize(target);
            }
            animationSheet = new TAnimation[(int)Priority.Count];
        }

        public void Play(string name,  Priority priority,  float speed = 1f) {
            TAnimation newAnim = animations.Get(name);
            animationSheet[(int)priority] = newAnim;
            playbackSpeed = speed;
            animate = true;
        }

        public void Pause() {
            animate = false;
        }

        public void Stop() {
            animate = false;

            // Keep only the default animation.
            for (int i = animationSheet.Length - 1; i >= 1; i--) {
                animationSheet[i] = null;
            }

            if (animationSheet[0] != null) {
                animationSheet[0].Reset();
            }
        }

        protected virtual void FixedUpdate() {
            if (!animate) { return; }
            
            TAnimation currAnim = GetHighestPriorityAnimation();
            if (!currAnim.hasTarget) { return; }

            currAnim.UpdateAnimation(playbackSpeed * Time.fixedDeltaTime);
            
            switch (currAnim.animEnd) {
                case AnimationEnd.Remove:
                    if (currAnim.finished)
                        Remove(currAnim);
                    break;
                case AnimationEnd.Pause:
                    if (currAnim.finished)
                        Pause();
                    break;
                case AnimationEnd.Loop:
                    if (currAnim.outsideLoop)
                        currAnim.Loop();
                    break;
                default:
                    break;
            }

        }

        public void Remove(string name) {
            TAnimation anim = animations.Get(name);
            Remove(anim);
        }

        protected void Remove(TAnimation anim) {
            if (anim == null) { return; }
            for (int i = animationSheet.Length - 1; i >= 0; i--) {
                if (animationSheet[i] == anim) {
                    animationSheet[i] = null;
                }
            }
        }

        private TAnimation GetHighestPriorityAnimation() {
            for (int i = animationSheet.Length - 1; i >= 0; i--) {
                if (animationSheet[i] != null) {
                    return animationSheet[i];
                }
            }
            return animationSheet[0];
        }

    }

}