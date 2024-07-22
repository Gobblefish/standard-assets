/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    [System.Serializable]
    public class SpriteAnimation {
        
        // The parameters of the animation.
        // [Header("Params")]
        [HideInInspector] public int currentFrame;
        public float fps;

        public Sprite[] sprites;
        [HideInInspector] public float ticks;
        private float t => (ticks * fps) % (float)sprites.Length;
        public float duration => fps == 0f ? 0f : (float)sprites.Length / fps; 

        // The frame of the current animation.
        // [Header("Frame")]

        //
        [Header("Rendering Order")]
        [HideInInspector] private int baseOffset;
        [HideInInspector] public int orderOffset;

        public void SetSpriteParams(SpriteRenderer spriteRenderer) {
            baseOffset = spriteRenderer.sortingOrder;
        }

        public void Tick(float dt) {
            ticks += Time.fixedDeltaTime;
        }

        public Sprite GetFrame() {
            currentFrame = (int)Mathf.Floor(t);
            return sprites[currentFrame];
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

        [SerializeField]
        private bool m_Loop = true;

        [SerializeField]
        private bool m_TurnOffOnEnd = false;

        [SerializeField]
        private bool m_RandomizeInitialTick = false;

        // The sprite attacted to this.
        private SpriteRenderer m_SpriteRenderer;

        // Runs once on instantiation.
        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_Animation.SetSpriteParams(m_SpriteRenderer);
            m_SpriteRenderer.sortingOrder += m_Animation.orderOffset;

            if (m_RandomizeInitialTick) {
                m_Animation.ticks = Randomizer.Range(0f, m_Animation.duration);
            }

        }

        void FixedUpdate() {
            if (!m_Animate) { return; }
            Animate(Time.fixedDeltaTime);
        }

        public void Animate(float dt) {
            m_Animation.Tick(dt);
            m_SpriteRenderer.sprite = m_Animation.GetFrame();

            if (!m_Loop && m_Animation.ticks > m_Animation.duration) {
                Stop();
                if (m_TurnOffOnEnd) {
                    gameObject.SetActive(false);
                }
            }

        }

        public static void Animate(SpriteRenderer spriteRenderer, SpriteAnimation animation, float dt) {
            animation.Tick(dt);
            spriteRenderer.sprite = animation.GetFrame();
        }

        public void SetAnimation(SpriteAnimation animation) {
            m_Animation = animation;
            m_Animation.ticks = Randomizer.Range(0f, 3f);
            if (m_SpriteRenderer != null && m_Animation != null && m_Animation.sprites.Length > 0) {
                m_SpriteRenderer.sortingOrder = m_Animation.GetOffset();
                Animate(0f);
            }        
        }

        public void SetFrameRate(float fps) {
            m_Animation.fps = fps;
        }

        public void PlayFromStart() {
            m_Animation.ticks = 0f;
            m_Animate = true;
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            }
        }

        public void Play() {
            m_Animate = true;
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            }
        }

        public void Pause() {
            m_Animate = false;
        }

        public void Stop() {
            m_Animate = false;
            if (m_SpriteRenderer != null && m_Animation != null && m_Animation.sprites.Length > 0) {
                m_SpriteRenderer.sprite = m_Animation.sprites[0];
            }
            if (gameObject.activeSelf) {
                gameObject.SetActive(false);
            }
        }

    }

}
