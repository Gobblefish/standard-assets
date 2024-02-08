// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Graphics;

namespace Gobblefish.Graphics {

    ///<summary>
    /// Ties the visual functionality to the rest of the game.
    ///<summary>
    public class GraphicsManager : MonoBehaviour {

        // The singleton.
        private static GraphicsManager INSTANCE; 

        [SerializeField]
        private GraphicsSettings m_Settings;
        public static GraphicsSettings Settings => INSTANCE.m_Settings;

        // The camera movement script.
        [SerializeField]
        private CameraMovement m_CamMovement;
        public static CameraMovement CamMovement => INSTANCE.m_CamMovement;
        
        // The camera shake script.
        [SerializeField]
        private CameraShake m_CamShake;
        public static CameraShake CamShake => INSTANCE.m_CamShake;

        public void SetSettings(GraphicsSettings settings) {
            m_Settings = settings;
        }

        void Awake() {
            // Application.targetFrameRate = VisualSettings.FrameRate;
            // m_CameraController.ReshapeWindow();
            // m_CameraController.RecolorScreen(m_DefaultPalette);
        }

    }

}