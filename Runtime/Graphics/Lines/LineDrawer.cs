// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    [RequireComponent(typeof(LineRenderer))]
    public class LineDrawer : MonoBehaviour, IPositionsReciever {

        // The line that this draws the catmull line with.
        [HideInInspector]
        protected LineRenderer m_LineRenderer;

        void Awake() {
            m_LineRenderer = GetComponent<LineRenderer>();
            m_LineRenderer.useWorldSpace = false;
        }

        public void RecievePositions(Vector3[] positions) {
            m_LineRenderer.positionCount = positions.Length;
            m_LineRenderer.SetPositions(positions);
        }

    }

}