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

        public static void QuitApplication() {
            // If you must programmatically quit an Android application, 
            // you can instead move the application to the background via
            // Activity.moveTaskToBack. 
            // For more information, refer to Quit a 
            // Unity Android application.

            // For iOS platform, in most cases the termination of 
            // application should be left at the user's discretion. 
            // Calling Application.Quit method in 
            // iOS Player might appear to the user that the application has crashed.

            Application.Quit();
        }

    }

}
