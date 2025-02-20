// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish {

    public class PopTest : MonoBehaviour {

        public float duration = 1f;
        public float ticks = 1f;

        public VisualEffects.PopVFX.Params popParams;

        void Update() {
            ticks -= Time.deltaTime;
            if (ticks <= 0f) {
                TestPop();
                ticks += duration;
            }
        }

        public void TestPop() {
            popParams.SetPositionToTransform(transform);
            VisualEffects.PopVFX.Play(popParams);
        }


    }

}
