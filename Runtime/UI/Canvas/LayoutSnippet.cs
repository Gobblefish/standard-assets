// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Layout {

    using Graphics;

    [System.Serializable]
    public class LayoutSnippet {

        // The name of this snippet.
        public string name;

        // Whether this snippet is assigned.
        public bool assigned => transform != null || rectTransform != null;

        // The transforms that this snippet takes
        public Transform transform = null;

        // The possible rect attacthed to this.
        [HideInInspector]
        public RectTransform rectTransform = null;

        // 
        public string animationName = "";

        // The animation with which this loads with.
        public TransformAnimation transformAnimation;

        // when this animation starts playing in the timeline.
        [SerializeField, Range(0f, 1f)] 
        public float normalizedStartTime = 0.2f;

        // When this animaion ends in the timeline.
        [SerializeField, Range(0f, 1f)] 
        public float normalizedEndTime = 0.6f;

        // Validate the layout snippet as being formatted alright.
        public bool Validate(float timelineDuration) {
            // Check if there is a rect.
            if (!assigned) { return false; }
            rectTransform = transform.GetComponent<RectTransform>();

            // Set the params.
            if (rectTransform != null) {
                transformAnimation.SetRectTransformParams(rectTransform);
            }
            transformAnimation.SetTransformParams(transform);
            
            // Set the duration of the animation.
            transformAnimation.duration = (normalizedEndTime - normalizedStartTime) * timelineDuration;

            // Make sure the times make sense.
            if (normalizedEndTime > normalizedStartTime) {
                return true;
            }
            return false;
        }

        public void Animate(float timelineTicks, float timelineDuration, float dt) {

            if (timelineTicks >= normalizedEndTime * timelineDuration - dt) {
                // Snap the animation to the end.
                if (rectTransform != null) {
                    TransformAnimator.SnapToOrigin(rectTransform,transformAnimation);
                }
                else {
                    TransformAnimator.SnapToOrigin(transform, transformAnimation);
                }               
            }
            else if (timelineTicks > normalizedStartTime * timelineDuration) {
                // Otherwise continue the animation.
                if (!transform.gameObject.activeSelf) {
                    transform.gameObject.SetActive(true);
                }
                if (rectTransform != null) {
                    TransformAnimator.Animate(rectTransform, transformAnimation, dt);
                }
                else {
                    TransformAnimator.Animate(transform, transformAnimation, dt);
                }
            }
        
        }

        public void SnapToOrigin() {
            // Snap the animation to the end.
            if (rectTransform != null) {
                TransformAnimator.SnapToOrigin(rectTransform, transformAnimation);
            }
            else {
                TransformAnimator.SnapToOrigin(transform, transformAnimation);
            } 
        }

    }

}