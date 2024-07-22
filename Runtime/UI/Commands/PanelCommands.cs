// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.UI {

    /// <summary>
    /// 
    /// </summary>
    public class PanelCommands : MonoBehaviour {

        public void Open() {
            if (gameObject == null) { Debug.Log("Doesn't have a game object"); }

            transform.SetParent(null);
            gameObject.SetActive(true);
        }
    
        public void Close() {
            gameObject.SetActive(false);
        }

    }

}