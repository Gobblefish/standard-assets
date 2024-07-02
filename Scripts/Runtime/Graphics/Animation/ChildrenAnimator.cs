/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Animation {

    using Random;

    [System.Serializable]
    public class ChildrenAnimation {
        
        [Header("Parames")]
        [HideInInspector] public float ticks;
        public float duration;
        public bool loop;
        // private float t => ;
        private float t(int i) => ((ticks + t0[i]) / duration) % 1;

        [HideInInspector] public int arrLength = 0; 
        [HideInInspector] public float[] t0;
        [HideInInspector] public Transform[] children;

        [Header("Position")]
        [HideInInspector] public Vector2[] basePosition;
        public Vector2 posScale;
        public AnimationCurve horPosCurve;
        public AnimationCurve vertPosCurve;

        [Header("Stretch")]
        [HideInInspector] public Vector2[] baseStretch;
        public Vector2 strectchScale;
        public AnimationCurve horStretchCurve;
        public AnimationCurve vertStretchCurve;

        [Header("Rotation")]
        [HideInInspector] public Quaternion[] baseRotation;
        public float rotationScale;
        public AnimationCurve rotationCurve;

        public void SetTransformParams(Transform transform) {

            arrLength = transform.childCount;
            t0 = new float[arrLength];
            children = new Transform[arrLength];
            basePosition = new Vector2[arrLength];
            baseStretch = new Vector2[arrLength];
            baseRotation = new Quaternion[arrLength];

            int i = 0;
            foreach (Transform child in transform) {
                children[i] = child;
                t0[i] = UnityEngine.Random.Range(0f, duration);
                basePosition[i] = child.localPosition; 
                baseRotation[i] = child.localRotation;
                baseStretch[i] = child.localScale;
                i += 1;
            }
            
        }

        public void SetTime(float t) {
            ticks = t;
            if (ticks > duration) {
                ticks = ticks % duration;
            }
        }

        public bool Tick(float dt) {
            ticks += Time.fixedDeltaTime;
            if (ticks > duration) {
                if (loop) {
                    ticks -= duration;
                }
                else {
                    ticks = 0f;
                    return false;
                }
            }
            return true;
        }


        public void SetPosition() {
            for (int i = 0; i < arrLength; i++) {
                children[i].localPosition = basePosition[i] + new Vector2(posScale.x * horPosCurve.Evaluate(t(i)), posScale.y * vertPosCurve.Evaluate(t(i)));
            }
        }

        public void SetStretch() {
            for (int i = 0; i < arrLength; i++) {
                children[i].localScale = baseStretch[i] + new Vector2(baseStretch[i].x * strectchScale.x * horStretchCurve.Evaluate(t(i)), baseStretch[i].y * strectchScale.y * vertStretchCurve.Evaluate(t(i)));
            }
        }

        public void SetRotation() {
            for (int i = 0; i < arrLength; i++) {
                children[i].localRotation = baseRotation[i] * Quaternion.Euler(0f, 0f, rotationScale * rotationCurve.Evaluate(t(i)));
            }
        }

        public ChildrenAnimation DeepCopy() {
            return null;
        }

    }

    public class ChildrenAnimator : MonoBehaviour {

        // The idle transform animation.
        [SerializeField]
        private ChildrenAnimation m_Animation;
        public ChildrenAnimation Animation => m_Animation;

        [SerializeField]
        private bool m_Animate = true;

        [SerializeField]
        private bool m_RandomizeInitialTick = false;

        // Runs once on instantiation.
        void Start() {
            m_Animation.SetTransformParams(transform);
            // if (m_RandomizeInitialTick) {
            //     m_Animation.SetTime(Randomizer.Range(0f, m_Animation.duration));
            // }
        }

        public void SetDuration(float duration) {
            float t = m_Animation.ticks/m_Animation.duration;
            m_Animation.duration = duration;
            m_Animation.ticks = t * m_Animation.duration;
        }

        public void Play() {
            m_Animate = true;
        }

        void FixedUpdate() {
            if (!m_Animate) { return; }
            Animate(Time.fixedDeltaTime);
        }

        public void Animate(float dt) {
            m_Animate = m_Animation.Tick(dt);
            m_Animation.SetPosition();
            m_Animation.SetRotation();
            m_Animation.SetStretch();
        }


    }

}