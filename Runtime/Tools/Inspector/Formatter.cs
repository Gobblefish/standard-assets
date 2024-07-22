// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using TMPro;

namespace Gobblefish {

    /// <summary>
    ///
    /// <summary>
    public abstract class ScriptableFormat : ScriptableObject {

    }

    /// <summary>
    /// Formats the components in a transform and its children to be consistent.
    /// Use this whenever you need to constantly change all things in a transform,
    /// But they all have the same thing they are changing to.
    /// Just a way of cutting down manual labour.  
    /// <summary>
    [ExecuteInEditMode]
    public abstract class Formatter<T, TFormat> : MonoBehaviour 
        where T : class
        where TFormat : ScriptableFormat
        {

        [SerializeField]
        protected TFormat m_Format;

        // Runs once every frame.
        void Update() {
            if (!Application.isPlaying && m_Format != null) {
                FormatAll();
            }
        }

        private void FormatAll() {

            // Format the text mesh attached to the transform as the header.
            T t = GetComponent<T>();
            if (t != null) {
                Format(t);
            }
            
            // Collect the text meshes under the transform not overridden by another formatter.
            List<T> tList = new List<T>();
            CollectComponents(transform, ref tList);
            Debug.Log(tList.Count);
            foreach (T _t in tList) {
                Format(_t);
            }

        }

        public abstract void Format(T tComponent);

        // Collects text meshes recursively unless STOPPED by another text formatter.
        public void CollectComponents(Transform transform, ref List<T> tList) {
            if (transform.childCount == 0) { return; }

            foreach (Transform child in transform)  {

                // Collect the text mesh if there is one.
                T t = child.GetComponent<T>();
                if (t != null && !tList.Contains(t)) {
                    tList.Add(t);
                }

                // Call recursively UNLESS overridden by another text editor.
                Formatter<T, TFormat> formatter = child.GetComponent<Formatter<T, TFormat>>();
                if (formatter == null) {
                    CollectComponents(child, ref tList);
                }

            }

        }

    }

}