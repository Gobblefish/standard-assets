/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Animation {

    [System.Serializable]
    public class SpriteAnimation {
        
        // The parameters of the animation.
        [Header("Params")]
        public Sprite[] sprites;
        public float ticks;
        private float t => (ticks * fps) % (float)sprites.Length;

        // The frame of the current animation.
        [Header("Frame")]
        public float fps;
        public int currentFrame;

        //
        [Header("Rendering Order")]
        private int baseOffset;
        public int orderOffset;

        public void SetSpriteParams(SpriteRenderer spriteRenderer) {
            baseOffset = spriteRenderer.sortingOrder;
        }

        public void Tick(float dt) {
            ticks += Time.fixedDeltaTime;
        }

        public Sprite GetFrame() {
            return sprites[(int)Mathf.Floor(t)];
        }

        public int GetOffset() {
            return baseOffset + orderOffset;
        }

    }

    ///<summary>
    ///
    ///<summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour {

        // The idle animation.
        [SerializeField]
        private SpriteAnimation m_Animation;

        // Whether or not this is animating.
        [SerializeField]
        private bool m_Animate = true;

        // The sprite attacted to this.
        private SpriteRenderer m_SpriteRenderer;



        // Runs once on instantiation.
        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_Animation.SetSpriteParams(m_SpriteRenderer);
            m_SpriteRenderer.sortingOrder += m_Animation.orderOffset;
        }

        void FixedUpdate() {
            if (!m_Animate) { return; }
            Animate(Time.fixedDeltaTime);
        }

        public void Animate(float dt) {
            m_Animation.Tick(dt);
            m_SpriteRenderer.sprite = m_Animation.GetFrame();
        }

        public static void Animate(SpriteRenderer spriteRenderer, SpriteAnimation animation, float dt) {
            animation.Tick(dt);
            spriteRenderer.sprite = animation.GetFrame();
        }

        public void SetAnimation(SpriteAnimation animation) {
            m_Animation = animation;
            m_Animation.ticks = Random.Range(0f, 3f);
            if (m_SpriteRenderer != null && m_Animation != null && m_Animation.sprites.Length > 0) {
                m_SpriteRenderer.sortingOrder = m_Animation.GetOffset();
                Animate(0f);
            }        
        }

        public void SetFrameRate(float fps) {
            m_Animation.fps = fps;
        }

        public void Play() {
            m_Animate = true;
        }

        public void Stop() {
            m_Animate = false;
            if (m_SpriteRenderer != null && m_Animation != null && m_Animation.sprites.Length > 0) {
                m_SpriteRenderer.sprite = m_Animation.sprites[0];
            }
        }

    }

}
