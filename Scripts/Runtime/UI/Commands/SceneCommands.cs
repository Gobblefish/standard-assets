// Unity.
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gobblefish {

    /// <summary>
    /// A wrapper object to organize objects that contains functionality that is useful to call from events
    /// The purpose of these is to be used to call events from the inspector.
    /// Do not add functionality meant to be called from within other scripts in here!
    /// <summary>
    public class SceneCommands : MonoBehaviour {

        // Loads a scene by the given scene name.
        public void LoadSceneByName(string sceneName) {
            SceneManager.LoadScene(sceneName);          
        }

        // Quits the application.
        public void QuitApplication() {
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
