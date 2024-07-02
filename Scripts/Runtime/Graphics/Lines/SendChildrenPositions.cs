// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    public class SendChildrenPositions : PositionsSender {
        
        [HideInInspector]
        private Vector3[] m_ChildPositions;

        [HideInInspector]
        private Transform[] m_Children;
        
        [HideInInspector]
        private int m_ChildCount = 0;

        void Awake() {
            m_ChildPositions = new Vector3[transform.childCount];
            m_Children = new Transform[transform.childCount];
            m_ChildCount = transform.childCount;

            int i = 0;
            foreach (Transform child in transform) {
                m_Children[i] = child;
                i += 1;
            }
        }

        void FixedUpdate() {
            for (int i = 0; i < m_ChildCount; i++) {
                m_ChildPositions[i] = m_Children[i].localPosition;
            }
            base.SendPositions(m_ChildPositions);
        }

    }

}