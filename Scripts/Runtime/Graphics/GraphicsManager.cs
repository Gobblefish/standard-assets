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
    public sealed class GraphicsManager : Manager<GraphicsManager, GraphicsSettings> {

        // The main camera of the game.
        private Camera m_Camera;
        public Camera MainCamera => m_Camera;

        // The camera movement script.
        [SerializeField]
        private CameraMovement m_CamMovement;
        public static CameraMovement CamMovement => Instance.m_CamMovement;
        
        // The camera shake script.
        [SerializeField]
        private CameraShake m_CamShake;
        public static CameraShake CamShake => Instance.m_CamShake;

        // The post processor controller.
        [SerializeField]
        private PostProcessorController m_PostProcessor;
        public PostProcessorController PostProcessor => Instance.m_PostProcessor;

        protected override void Awake() {
            m_Camera = Camera.main;
            m_Settings = new GraphicsSettings();
            base.Awake();
        }

    }

}