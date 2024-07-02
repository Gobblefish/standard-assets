// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    public abstract class Randomizer : MonoBehaviour {

        public abstract void Randomize();
        
        public static float Range(float min, float max) {
            return UnityEngine.Random.Range(min, max);
        }

        public static int Range(int min, int max) {
            return UnityEngine.Random.Range(min, max);
        }

    }

}