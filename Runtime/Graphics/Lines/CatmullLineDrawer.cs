// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Graphics {

    // a single catmull-rom curve
    public struct CatmullRomCurve {
        public Vector2 p0, p1, p2, p3;
        public float alpha;

        public CatmullRomCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float alpha) {
            (this.p0, this.p1, this.p2, this.p3) = (p0, p1, p2, p3);
            this.alpha = alpha;
        }

        // Evaluates a point at the given t-value from 0 to 1
        public Vector2 GetPoint(float t) {
            // calculate knots
            const float k0 = 0;
            float k1 = GetKnotInterval(p0, p1);
            float k2 = GetKnotInterval(p1, p2) + k1;
            float k3 = GetKnotInterval(p2, p3) + k2;

            // evaluate the point
            float u = Mathf.LerpUnclamped(k1, k2, t);
            Vector2 A1 = Remap(k0, k1, p0, p1, u);
            Vector2 A2 = Remap(k1, k2, p1, p2, u);
            Vector2 A3 = Remap(k2, k3, p2, p3, u);
            Vector2 B1 = Remap(k0, k2, A1, A2, u);
            Vector2 B2 = Remap(k1, k3, A2, A3, u);
            return Remap(k1, k2, B1, B2, u);
        }

        static Vector2 Remap(float a, float b, Vector2 c, Vector2 d, float u) {
            return Vector2.LerpUnclamped(c, d, (u - a) / (b - a));
        }

        float GetKnotInterval(Vector2 a, Vector2 b) {
            return Mathf.Pow(Vector2.SqrMagnitude(a - b), 0.5f * alpha);
        }

    }

    [RequireComponent(typeof(LineRenderer))]
    public class CatmullLineDrawer : MonoBehaviour, IPositionsReciever {

        // The line that this draws the catmull line with.
        [HideInInspector]
        protected LineRenderer m_LineRenderer;

        // The 
        [SerializeField]
        private Vector3[] m_CatmullPositions;

        [SerializeField]
        private int m_Subdivisions;

        void Awake() {
            m_LineRenderer = GetComponent<LineRenderer>();
            m_LineRenderer.useWorldSpace = false;
        }

        public void RecievePositions(Vector3[] positions) {
            if (positions.Length == 4) { 
                CatmullizeFourPointPositionArray(positions); 
            }
            else {
                CatmullizePositionArray(positions);
            }
            m_LineRenderer.positionCount = m_CatmullPositions.Length;
            m_LineRenderer.SetPositions(m_CatmullPositions);
        }

        // The single case.
        public void CatmullizeFourPointPositionArray(Vector3[] p) {
            CatmullRomCurve romCurve = new CatmullRomCurve(p[0], p[1], p[2], p[3], 0.5f);

            if (m_CatmullPositions == null || m_CatmullPositions.Length != m_Subdivisions) {
                m_CatmullPositions = new Vector3[m_Subdivisions];
            }

            float r = 0f;
            float f_subdivsions = (float)m_Subdivisions;
            for (int j = 0; j < m_Subdivisions; j++) {
                r = (float)j / f_subdivsions;
                m_CatmullPositions[j] = romCurve.GetPoint(r);
            }
        }

        // The multiple case.
        public void CatmullizePositionArray(Vector3[] p) {
            List<CatmullRomCurve> romCurves = new List<CatmullRomCurve>();
            for (int i = 0; i < p.Length - 3; i++) {
                romCurves.Add(new CatmullRomCurve(p[i], p[i+1], p[i+2], p[i+3], 0.5f));
            }
             
            if (m_CatmullPositions == null || m_CatmullPositions.Length != m_Subdivisions) {
                m_CatmullPositions = new Vector3[m_Subdivisions];
            }

            m_CatmullPositions[0] = p[0];

            // P = (1−t)2P1 + 2(1−t)tP2 + t2P3
            float r = 0f;
            float f_subdivisions = (float)m_Subdivisions;
            int n = 0;
            float t = 0f;
            for (int j = 1; j < m_Subdivisions; j++) {
                r = (float)j / f_subdivisions;

                t = r * romCurves.Count;
                n = (int)Mathf.Floor(t);

                m_CatmullPositions[j] = romCurves[n].GetPoint(t - n);

            }

        }

        

    }

}