/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish {

    public static class CollectionsExtensions {

        // Validate an array.
        public static bool Validate<T>(this T[] array) {
            return array != null && array.Length > 0;
        }

        // Validate a list.
        public static bool Validate<T>(this List<T> list) {
            return list != null && list.Count > 0;
        }

        public static List<T> Clean<T>(this List<T> list) {
            list = list.FindAll(item => item != null);
            return list;
        }

        public static List<TBehaviour> CleanObjects<TBehaviour>(this List<TBehaviour> list) where TBehaviour : MonoBehaviour {
            list = list.FindAll(item => item != null && item.gameObject != null);
            return list;
        }

        public static List<TBehaviour> DestroyAppropriately<TBehaviour>(this List<TBehaviour> list) where TBehaviour : MonoBehaviour {
            list.CleanObjects();
            for (int i = 0; i < list.Count; i++) {
                list[i].gameObject.DestroyAppropriately();
            }
            return new List<TBehaviour>();
        }

    }

    public static class ComponentExtensions {

        public static T Require<T>(this MonoBehaviour mb) 
            where T : MonoBehaviour {
            T t = mb.GetComponent<T>();
            if (t == null) {
                return mb.gameObject.AddComponent<T>();
            }
            return t;
        }

        public static void DestroyAppropriately(this GameObject gameObject) {
            if (!Application.isPlaying) { GameObject.DestroyImmediate(gameObject); }
            else { GameObject.Destroy(gameObject); }
        }

        // public static List<T> CollectAllU



    }

}
