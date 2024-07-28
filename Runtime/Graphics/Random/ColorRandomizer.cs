// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Graphics {

    using Random = UnityEngine.Random;

    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorRandomizer : Randomizer {

        [SerializeField]
        private Gradient m_Gradient;

        //
        private SpriteRenderer m_SpriteRenderer;

        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            Randomize();
        }

        public override void Randomize() {
            m_SpriteRenderer.color = m_Gradient.Evaluate(Random.Range(0f, 1f));
        }

    }

}