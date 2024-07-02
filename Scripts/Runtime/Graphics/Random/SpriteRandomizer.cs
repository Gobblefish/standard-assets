// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Random {

    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRandomizer : Randomizer {

        [SerializeField]
        private Sprite[] m_Sprites;

        //
        private SpriteRenderer m_SpriteRenderer;

        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            Randomize();
        }

        public override void Randomize() {
            m_SpriteRenderer.sprite = m_Sprites[Range(0, m_Sprites.Length)];
        }

    }

}