// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Graphics {

    using Random = UnityEngine.Random;

    public class RotationRandomizer : Randomizer {

        [SerializeField]
        private Quaternion m_BaseRotation;

        [SerializeField]
        private float m_AngleRange;

        void Start() {
            m_BaseRotation = transform.localRotation;
            Randomize();
        }

        public override void Randomize() {
            transform.localRotation = m_BaseRotation * Quaternion.Euler(0f, 0f, Random.Range(-m_AngleRange, m_AngleRange));
        }

    }

}