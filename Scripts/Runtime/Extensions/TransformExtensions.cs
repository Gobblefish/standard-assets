/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Extensions {

    public static class TransformExtensions {

        public static void FromMatrix(this Transform transform, Matrix4x4 matrix) {
            transform.localScale = matrix.ExtractScale();
            transform.rotation = matrix.ExtractRotation();
            // transform.position = ExtractPosition(matrix);
        }

    }

}
