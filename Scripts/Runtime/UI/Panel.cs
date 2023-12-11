// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEditor;
// Gobblefish
using Gobblefish.Audio;

namespace Gobblefish.UI {

    /// <summary>
    /// An easy component to be able to control the volume of a clip.
    /// </summary>
    public class Panel : MonoBehaviour {

        public void Open() {
            transform.SetParent(null);
            gameObject.SetActive(true);
        }
    
        public void Close() {
            gameObject.SetActive(false);
        }

    }

}