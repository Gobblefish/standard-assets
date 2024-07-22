/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gobblefish.Graphics {

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class MaterialFloatAnimation {

        // Just to see how well this updates.
        
        // The parameters of the animation.
        [Header("Params")]
        public float ticks;
        public float duration;
        private float t => ticks / duration;

        // How the intensity varies.
        [Header("VariableName")]
        public string varName;
        
        private float baseVal;
        public float fluxScale;
        public AnimationCurve fluxCurve;

        // Set the values from the lights.
        public void GetValueParams(Material mat) {
            baseVal = mat.GetFloat(varName);
        }

        // Set the time of the animation.
        public void SetTime(float t) {
            ticks = t;
            if (ticks > duration) {
                ticks = ticks % duration;
            }
            else if (ticks < 0f) {
                ticks += duration;
            }
        }

        // Tick the animation.
        public void Tick(float dt) {
            ticks += dt;
            if (ticks > duration) {
                ticks -= duration;
            }
            else if (ticks < 0f) {
                ticks += duration;
            }

        }

        public float GetValue() {
            return baseVal + fluxScale * fluxCurve.Evaluate(t);
        }

    }

    ///<summary>
    ///
    ///<summary>
    public class MaterialAnimator : MonoBehaviour {

        // The idle light animation.
        [SerializeField]
        private MaterialFloatAnimation[] m_FloatAnimations;

        [SerializeField]
        private bool m_Animate;

        [SerializeField]
        private float m_PlayDirection = 1f;

        // The light attacted to this.
        [SerializeField]
        private SpriteRenderer m_SpriteRenderer;

        [SerializeField]
        private SpriteRenderer[] m_ExtraRenderers;

        [SerializeField]
        private UnityEngine.U2D.SpriteShapeRenderer[] m_ExtraShapeRenderers;

        // Runs once on instantiation.
        void Start() {
            if (m_SpriteRenderer == null) {
                m_SpriteRenderer = GetComponent<SpriteRenderer>();
            }
            for (int i = 0; i < m_FloatAnimations.Length; i++) {
                m_FloatAnimations[i].GetValueParams(m_SpriteRenderer.material);
            } 
        }

        void FixedUpdate() {
            if (!m_Animate) { return; }
            Animate(m_PlayDirection * Time.fixedDeltaTime);
        }

        public void Animate(float dt) {
            for (int i = 0; i < m_FloatAnimations.Length; i++) {
                m_FloatAnimations[i].Tick(dt);
                m_SpriteRenderer.material.SetFloat(m_FloatAnimations[i].varName, m_FloatAnimations[i].GetValue());
                for (int j = 0; j < m_ExtraRenderers.Length; j++) {
                    m_ExtraRenderers[j].sharedMaterial = m_SpriteRenderer.sharedMaterial;
                }
                for (int j = 0; j < m_ExtraShapeRenderers.Length; j++) {
                    m_ExtraShapeRenderers[j].materials[0] = m_SpriteRenderer.sharedMaterial;
                    m_ExtraShapeRenderers[j].materials[1] = m_SpriteRenderer.sharedMaterial;
                }
            } 
        }

        // public static void Animate(Light2D light, LightAnimation animation, float dt) {
        //     animation.Tick(dt);
        //     //
        // }

        public void Play() {
            m_Animate = true;
            m_PlayDirection = 1f;
            for (int i = 0; i < m_FloatAnimations.Length; i++) {
                m_FloatAnimations[i].ticks = 0f;
            }
            // if (!gameObject.activeSelf) {
            //     gameObject.SetActive(true);
            // }
        }

        public void PlayBackwards() {
            m_Animate = true;
            m_PlayDirection = -1f;
            for (int i = 0; i < m_FloatAnimations.Length; i++) {
                m_FloatAnimations[i].ticks = m_FloatAnimations[i].duration;
            }
            // if (!gameObject.activeSelf) {
            //     gameObject.SetActive(true);
            // }
        }

        public void Pause() {
            m_Animate = false;
        }

        public void Stop() {
            m_Animate = false;
            // if (m_SpriteRenderer != null && m_Animation != null && m_Animation.sprites.Length > 0) {
            //     m_SpriteRenderer.sprite = m_Animation.sprites[0];
            // }
            // if (gameObject.activeSelf) {
            //     gameObject.SetActive(false);
            // }
        }

    }

}
