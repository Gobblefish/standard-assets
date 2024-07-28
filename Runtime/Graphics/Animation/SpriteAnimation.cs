// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    [CreateAssetMenu(fileName="SpriteAnimation", menuName="Animations/Sprites")]
    public class SpriteAnimation : Animation<SpriteRenderer> {
        
        // The sprites in this animation.
        protected Sprite originalSprite;
        public Sprite[] sprites;

        // The frames per second.
        public float fps;

        // The current frame based on the ticks.
        private int currentFrame => (int)Mathf.Floor((ticks * fps) % (float)sprites.Length);
        // public float duration => fps == 0f ? 0f : (float)sprites.Length / fps; 

        // Initializes the base values of the transform.
        protected override void InitializeAnimationTarget() {
            originalSprite = target.sprite;
        }

        // Updates the transform parameters.
        protected override void UpdateAnimationTarget() {
            target.sprite = sprites[currentFrame];
        }

        protected override void ResetAnimationTarget() { 
            target.sprite = originalSprite;
        }

    }

}
