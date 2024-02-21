/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Extensions {

    public static class BoundsIntExtensions {

        public static BoundsInt Pad(this BoundsInt bounds, int padding) {
            Vector3Int min = bounds.min;
            min -= new Vector3Int(padding, padding, 0);
            Vector3Int max = bounds.max;
            max += new Vector3Int(padding, padding, 0);
            bounds.SetMinMax(min, max);
            return bounds;
        }

    }

}
