// System.
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// Unity.
using UnityEngine;

namespace GobbleFish.Graphics {

    public class SendTrailPositions : PositionsSender {

        // The positions along the trail.
        private List<Vector3> m_Positions;

        // The last cached position of this transform.
        private Vector3 m_CachedPositions;

        // The duration after which the trail fades. 
        [SerializeField] 
        private float m_FadeInterval;
        
        // The precision of the trail.
        [SerializeField] 
        private float m_SegmentLength;

        // Runs once before the first frame.
        void Start() {
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
            base.SendPositions(m_Positions.ToArray());
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
            base.SendPositions(m_Positions.ToArray());
        }

    }
    
}