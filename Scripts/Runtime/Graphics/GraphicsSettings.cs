// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    ///<summary>
    /// The settings for using the visuals in the game.
    ///<summary>
    [System.Serializable]
    public class GraphicsSettings : Settings<GraphicsSettings> {

        // The amount of camera shake.
        public float camShakeStrength = 1f;

        // Whether camera shake is enabled.
        public bool camShakeDisabled = false;

        // The actual camera shake value.
        public float camShake => camShakeDisabled ? 0f : camShakeStrength;

        // Whether to limit the frame rate or let it run at max.
        public bool limitFrameRate = false;

        // The target frame rate this game runs at.
        public float targetFrameRate = 60f;

        // The actual frame rate the game should run at.
        public float frameRate => limitFrameRate ? -1f : targetFrameRate;


    }

}