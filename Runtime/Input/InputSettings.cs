// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Input {

    [System.Serializable]
    public class InputSettings : Settings<InputSettings> {
        
        public KeyCode[] directionKeybinds = new KeyCode[4] {
            KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D
        };

        public KeyCode[] actionKeybinds = new KeyCode[3] {
            KeyCode.Space, KeyCode.J, KeyCode.K
        };

        // public KeyboardInputSettings() {
        //     this.directionKeybinds = 
        //     this.actionKeybinds = 
        // }

    }

}