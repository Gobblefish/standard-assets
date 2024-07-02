/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    [RequireComponent(typeof(LineRenderer))]
    public class TrailAnimator : MonoBehaviour {

        // The line renderer attached to this trail.
        private LineRenderer m_LineRenderer;

        // The positions along the trail.
        private List<Vector3> m_Positions;

        // The last cached position of this transform.
        private Vector3 m_CachedPositions;

        // The width of this trail..
        [SerializeField] 
        private float m_Width;
        
        // The duration after which the trail fades. 
        [SerializeField] 
        private float m_FadeInterval;
        
        // The precision of the trail.
        [SerializeField] 
        private float m_SegmentLength;
        

        // Runs once before the first frame.
        void Start() {
            m_LineRenderer = GetComponent<LineRenderer>();
            m_LineRenderer.endWidth = 0f;
            m_LineRenderer.startWidth = m_Width;
            m_Positions = new List<Vector3>();
        }

        // Runs every time this object is enabled.
        void OnEnable() {
            m_Positions = new List<Vector3>();
        }

        // Runs once every frame.
        void FixedUpdate() {
            float dx = (m_CachedPositions - transform.position).magnitude;
            if (dx > m_SegmentLength) {
                AddPosition();
            }
        }

        // Adds a point
        void AddPosition() {
            m_Positions.Insert(0, transform.position);
            m_CachedPositions = transform.position;
            m_LineRenderer.positionCount = m_Positions.Count;
            m_LineRenderer.SetPositions(m_Positions.ToArray());
            StartCoroutine(IERemove());
        }

        // Removes the end of the trail.
        private IEnumerator IERemove() {
            yield return new WaitForSeconds(m_FadeInterval);
            if (m_Positions.Count > 0) {
                m_Positions.RemoveAt(m_Positions.Count - 1);
            }
            else if (m_Positions.Count == 1) {
                m_Positions.RemoveAt(0);
            }
            m_LineRenderer.positionCount = m_Positions.Count;
            m_LineRenderer.SetPositions(m_Positions.ToArray());
        }

    }
    
}