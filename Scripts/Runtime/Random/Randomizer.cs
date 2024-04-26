// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Random {

    public abstract class Randomizer : MonoBehaviour {

        public abstract void Randomize();
        
        public float Range(float min, float max) {
            return UnityEngine.Random.Range(min, max);
        }

        public int Range(int min, int max) {
            return UnityEngine.Random.Range(min, max);
        }

    }

}