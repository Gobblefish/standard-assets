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

        // The main camera of the game.
        private Camera m_Camera;
        public Camera MainCamera => m_Camera;

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

        // The post processor controller.
        [SerializeField]
        private PostProcessorController m_PostProcessor;
        public PostProcessorController PostProcessor => m_PostProcessor;
        public static PostProcessorController POSTPROCESSOR => INSTANCE.m_PostProcessor;

        public void SetSettings(GraphicsSettings settings) {
            m_Settings = settings;
        }

        void Awake() {
            // Application.targetFrameRate = VisualSettings.FrameRate;
            // m_CameraController.ReshapeWindow();
            // m_CameraController.RecolorScreen(m_DefaultPalette);
            INSTANCE = this;
            m_Camera = Camera.main;
        }

    }

}