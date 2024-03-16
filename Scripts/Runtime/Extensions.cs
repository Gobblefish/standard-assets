/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish {

    public static class CollectionsExtensions {

        // Validate an array.
        public static bool Validate<T>(this T[] array) {
            return array != null && array.Length > 0;
        }

        // Validate a list.
        public static bool Validate<T>(this List<T> list) {
            return list != null && list.Count > 0;
        }

        public static void Clean<T>(this List<T> list) {
            list = list.FindAll(item => item != null);

        public static void CleanObjects<TBehaviour>(this List<TBehaviour> list) where TBehaviour : MonoBehaviour {
            list = list.FindAll(item => item != null && item.gameObject != null);
        }

    }

    public static class ComponentExtensions {

        public static T GetOrAdd<T>(this MonoBehaviour mb) {
            T t = mb.GetComponent<T>() == null;
            if (t == null) {
                return mb.gameObject.AddComponent<T>();
            }
            return t;
        }

    }

}
