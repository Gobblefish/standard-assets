/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Animation {

    [System.Serializable]
    public class TransformAnimation {
        
        [Header("Parames")]
        private float ticks;
        public float duration;
        public bool loop;
        private float t => ticks / duration;

        [Header("Position")]
        [HideInInspector] public Vector2 basePosition;
        [HideInInspector] public Vector2 baseAnchor;
        public Vector2 posScale;
        public AnimationCurve horPosCurve;
        public AnimationCurve vertPosCurve;

        [Header("Stretch")]
        [HideInInspector] public Vector2 baseStretch;
        public Vector2 strectchScale;
        public AnimationCurve horStretchCurve;
        public AnimationCurve vertStretchCurve;

        [Header("Rotation")]
        [HideInInspector] public float baseRotation;
        public float rotationScale;
        public AnimationCurve rotationCurve;

        public void SetRectTransformParams(RectTransform rt) {
            baseAnchor = rt.anchoredPosition;
        }

        public void SetTransformParams(Transform transform) {
            basePosition = transform.localPosition; 
            baseRotation = transform.eulerAngles.z;
            baseStretch = transform.localScale;
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


        public Vector3 GetPosition() {
            return basePosition + new Vector2(posScale.x * horPosCurve.Evaluate(t), posScale.y * vertPosCurve.Evaluate(t));
        }

        public Vector3 GetRectPosition() {
            Camera cam = Camera.main;

            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            float xScale = cam.pixelWidth / width;
            float yScale = cam.pixelHeight / height;

            return baseAnchor + new Vector2(xScale * posScale.x * horPosCurve.Evaluate(t), yScale * posScale.y * vertPosCurve.Evaluate(t));
        }

        public Vector3 GetPosition(float T) {
            return basePosition + new Vector2(posScale.x * horPosCurve.Evaluate(T), posScale.y * vertPosCurve.Evaluate(T));
        }

        public Vector3 GetStretch() {
            return baseStretch + new Vector2(strectchScale.x * horStretchCurve.Evaluate(t), strectchScale.y * vertStretchCurve.Evaluate(t));
        }

        public Quaternion GetRotation() {
            return Quaternion.Euler(0f, 0f, baseRotation + rotationScale * rotationCurve.Evaluate(t));
        }

    }

    public class TransformAnimator : MonoBehaviour {

        // The idle transform animation.
        [SerializeField]
        private TransformAnimation m_Animation;
        public TransformAnimation Animation => m_Animation;

        [SerializeField]
        private bool m_Animate = true;

        // Runs once on instantiation.
        void Start() {
            m_Animation.SetTransformParams(transform);
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
            transform.localPosition = m_Animation.GetPosition();
            transform.localRotation = m_Animation.GetRotation();
            transform.localScale = m_Animation.GetStretch();
        }

        public static void Animate(Transform transform, TransformAnimation animation, float dt) {
            animation.Tick(dt);
            transform.localPosition = animation.GetPosition();
            transform.localRotation = animation.GetRotation();
            transform.localScale = animation.GetStretch();
        }

        public static void Animate(RectTransform rt, TransformAnimation animation, float dt) {
            animation.Tick(dt);
            rt.anchoredPosition = animation.GetRectPosition();
            // transform.localRotation = animation.GetRotation();
            // transform.localScale = animation.GetStretch();
        }

        public void SnapToOrigin() {
            SnapToOrigin(transform, m_Animation);
        }

        public static void SnapToOrigin(Transform transform, TransformAnimation animation) {
            transform.localScale = animation.baseStretch;
            transform.localPosition = animation.basePosition;
            transform.localRotation = Quaternion.Euler(0f, 0f, animation.baseRotation);
        }

        public static void SnapToOrigin(RectTransform rt, TransformAnimation animation) {
            rt.localPosition = animation.basePosition;
            // rt.localScale = animation.baseStretch;
            // rt.localRotation = Quaternion.Euler(0f, 0f, animation.baseRotation);
        }

    }

}