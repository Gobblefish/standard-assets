// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Animations {

    public class MaterialAnimator : Animator<MaterialAnimation, Material> {
        
        // Runs once before the first frame.
        protected override void Start() {
            target = GetComponent<SpriteRenderer>().material;
            base.Start();
        }

    }

}