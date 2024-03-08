/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Extensions {

    public static class CollectionsExtensions {

        // Validate an array.
        public static bool Validate<T>(this T[] array) {
            return array != null && array.Length > 0;
        }

        // Validate a list.
        public static bool Validate<T>(this List<T> list) {
            return list != null && list.Count > 0;
        }
    }

}
