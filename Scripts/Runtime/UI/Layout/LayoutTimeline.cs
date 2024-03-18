// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Layout;


namespace Gobblefish.Layout {

    /// <summary>
    /// Controls the order and manner in which the background loads.
    /// </summary>
    [ExecuteInEditMode]
    public class LayoutTimeline : MonoBehaviour {

        // The duration for the loading to take.
        [SerializeField]
        private float m_Duration;

        // The position within the timeline. 
        [SerializeField]
        private float m_Ticks = 0f;

        // The snippets in the background.
        [SerializeField]
        private LayoutSnippet[] m_Snippets;

        [SerializeField]
        private bool m_Playing = true;

        // Runs once on instantiation.
        void Awake() {
            if (!Application.isPlaying) { return; }

            for (int i = 0; i < m_Snippets.Length; i++) {
                // Set the transform parameters.
                m_Snippets[i].Validate(m_Duration);

                // Snap the snippets to their starting position.
                m_Snippets[i].transformAnimation.SetTime(0f);
                m_Snippets[i].Animate(0f, m_Duration, 0f);

                m_Snippets[i].transform.gameObject.SetActive(false);
            }
        }

        // Runs once every frame.
        void Update() {
            if (!Application.isPlaying) {
                for (int i = 0; i < m_Snippets.Length; i++) {
                    string name = m_Snippets[i].transform != null ? m_Snippets[i].transform.gameObject.name : (m_Snippets[i].rectTransform != null ? m_Snippets[i].rectTransform.gameObject.name : "unassigned");
                    m_Snippets[i].name = name;
                }
            }
        }

        // Runs once every fixed interval.
        void FixedUpdate() {
            if (Application.isPlaying && m_Playing) {
                Play(Time.fixedDeltaTime);
            }
        }

        // Plays through the timeline.
        private void Play(float dt) {
            m_Ticks += dt;

            // End the animaion if necessary.
            if (m_Ticks >= m_Duration) {
                m_Playing = false;
                
                // Snap all of them to the end positions.
                m_Ticks = m_Ticks - 0.02f;
                for (int i = 0; i < m_Snippets.Length; i++) {
                    if (m_Snippets[i].assigned) {
                        m_Snippets[i].Animate(m_Ticks, m_Duration, 0f);
                        // m_Snippets[i].SnapToOrigin();
                    }
                }

                return;
            }

            // Itterate through the snippets and animate them appropriately.
            for (int i = 0; i < m_Snippets.Length; i++) {
                if (m_Snippets[i].assigned) {
                    m_Snippets[i].Animate(m_Ticks, m_Duration, dt);
                }
            }

        }
        
    }

}