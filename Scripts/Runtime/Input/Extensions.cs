/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Input {

    /// <summary>
    /// 
    /// </summary>
    public static class BoolExtensions {

        public static int ToInt(this bool p) {
            return p ? 1 : 0;
        }

    }

}
