// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    using Random = UnityEngine.Random;

    public class PositionRandomizer : Randomizer {

        [SerializeField]
        private Vector2 m_Box;

        [SerializeField]
        private Vector2 m_Offset;

        //
        private Vector3 m_Origin;

        void Start() {
            m_Origin = transform.localPosition;
            Randomize();
        }

        public override void Randomize() {
            transform.localPosition = m_Origin + (Vector3)m_Offset + new Vector3(Random.Range(-m_Box.x, m_Box.x),Random. Range(-m_Box.y, m_Box.y), 0f);
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            if (Application.isPlaying) {
                Gizmos.DrawWireCube((Vector3)m_Offset + m_Origin, (Vector3)m_Box);
            }
            else {
                Gizmos.DrawWireCube((Vector3)m_Offset + transform.position, (Vector3)m_Box);
            }
        }

    }

}