// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gobblefish {

    [CreateAssetMenu(fileName="SceneSwitch", menuName="SceneSwitch")]
    public class SceneSwitch : ScriptableObject {

        public void SwitchScenes(string sceneName) {
            SceneManager.LoadScene(sceneName);          
        }

        public static void GoTo(string sceneName) {
            SceneManager.LoadScene(sceneName);          
        }

    }

}
