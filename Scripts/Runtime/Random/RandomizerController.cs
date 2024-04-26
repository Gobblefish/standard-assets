// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Random {

    public class RandomizerController: MonoBehaviour {

        [SerializeField]
        private Randomizer[] m_Randomizers;

        public bool randomize = false;

        void Start() {
            m_Randomizers = transform.GetComponentsInChildren<Randomizer>();
        }

        void Update() {
            if (randomize) {
                Randomize();
                randomize = false;
            }
        }

        public void Randomize() {
            foreach (Randomizer randomizer in m_Randomizers) {
                randomizer.Randomize();
            }
        }

    }

}