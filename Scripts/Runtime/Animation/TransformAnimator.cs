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
        [HideInInspector] public float ticks;
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
        [HideInInspector] public Quaternion baseRotation;
        public float rotationScale;
        public AnimationCurve rotationCurve;

        public void SetRectTransformParams(RectTransform rt) {
            baseAnchor = rt.anchoredPosition;
        }

        public void SetTransformParams(Transform transform) {
            basePosition = transform.localPosition; 
            baseRotation = transform.localRotation;
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

        public Vector3 GetPosition(Vector3 position) {
            return position + new Vector3(posScale.x * horPosCurve.Evaluate(t), posScale.y * vertPosCurve.Evaluate(t), 0f);
        }

        public Vector3 GetStretch() {
            return baseStretch + new Vector2(baseStretch.x * strectchScale.x * horStretchCurve.Evaluate(t), baseStretch.y * strectchScale.y * vertStretchCurve.Evaluate(t));
        }

        public Vector3 GetStretch(Vector3 stretch) {
            return stretch + new Vector3(posScale.x * horPosCurve.Evaluate(t), posScale.y * vertPosCurve.Evaluate(t), 0f);
        }

        public Quaternion GetRotation() {
            return baseRotation * Quaternion.Euler(0f, 0f, rotationScale * rotationCurve.Evaluate(t));
        }

        public Quaternion GetRotation(Quaternion rotation) {
            return rotation * Quaternion.Euler(0f, 0f, rotationScale * rotationCurve.Evaluate(t));
        }

        public TransformAnimation DeepCopy() {
            TransformAnimation newAnimation = new TransformAnimation();

            newAnimation.baseAnchor = this.baseAnchor;
            newAnimation.baseStretch = this.baseStretch;
            newAnimation.baseRotation = this.baseRotation;
            newAnimation.baseRotation = this.baseRotation;

            newAnimation.ticks = this.ticks;
            newAnimation.duration = this.duration;
            newAnimation.loop = this.loop;

            newAnimation.posScale = this.posScale;
            newAnimation.horPosCurve = this.horPosCurve;
            newAnimation.vertPosCurve = this.vertPosCurve;

            newAnimation.strectchScale = this.strectchScale;
            newAnimation.horStretchCurve = this.horStretchCurve;
            newAnimation.vertStretchCurve = this.vertStretchCurve;

            newAnimation.rotationScale = this.rotationScale;
            newAnimation.rotationCurve = this.rotationCurve;
            
            return newAnimation;
        }

    }

    public class TransformAnimator : MonoBehaviour {

        // The idle transform animation.
        [SerializeField]
        private TransformAnimation m_Animation;
        public TransformAnimation Animation => m_Animation;

        [SerializeField]
        private bool m_Animate = true;

        [SerializeField]
        private bool m_RandomizeInitialTick = false;

        // Runs once on instantiation.
        void Start() {
            m_Animation.SetTransformParams(transform);
            if (m_RandomizeInitialTick) {
                m_Animation.SetTime(Random.Range(0f, m_Animation.duration));
            }
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
            transform.localPosition = m_Animation.GetPosition();
            transform.localRotation = m_Animation.GetRotation();
            transform.localScale = m_Animation.GetStretch();
        }

        public static void Animate(Transform transform, Matrix4x4 baseMatrix, TransformAnimation animation, float T) {
            animation.SetTime(T);
            transform.localPosition = animation.GetPosition(baseMatrix.ExtractPosition());
            transform.localRotation = animation.GetRotation(baseMatrix.ExtractRotation());
            transform.localScale = animation.GetStretch(baseMatrix.ExtractScale());
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
            rt.transform.localRotation = animation.GetRotation();
            rt.transform.localScale = animation.GetStretch();
        }

        public void SnapToOrigin() {
            SnapToOrigin(transform, m_Animation);
        }

        public static void SnapToOrigin(Transform transform, TransformAnimation animation) {
            transform.localScale = animation.baseStretch;
            transform.localPosition = animation.basePosition;
            transform.localRotation = animation.baseRotation;
        }

        public static void SnapToOrigin(RectTransform rt, TransformAnimation animation) {
            // rt.localPosition = animation.basePosition;
            // rt.localScale = animation.baseStretch;
            // rt.localRotation = Quaternion.Euler(0f, 0f, animation.baseRotation);
        }

    }

}