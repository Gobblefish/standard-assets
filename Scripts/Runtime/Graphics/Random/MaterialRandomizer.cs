// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Random {

    [RequireComponent(typeof(SpriteRenderer))]
    public class MaterialRandomizer : Randomizer {

        [SerializeField]
        private Material[] m_Materials;

        //
        private SpriteRenderer m_SpriteRenderer;

        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            Randomize();
        }

        public override void Randomize() {
            m_SpriteRenderer.sharedMaterial = m_Materials[Range(0, m_Materials.Length)];
        }

    }

}