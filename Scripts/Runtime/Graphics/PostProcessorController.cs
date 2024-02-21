/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

namespace Gobblefish.Graphics {

    ///<summary>
    /// Controls the position and quality of the camera.
    ///<summary>
    public class PostProcessorController : MonoBehaviour {

        [SerializeField]
        private Volume m_Volume;
        private VolumeProfile m_DefaultProfile;

        void Awake() {
            m_DefaultProfile = m_Volume.sharedProfile;
        }

        public void SetVolumeProfile(VolumeProfile volumeProfile) {
            m_Volume.sharedProfile = volumeProfile;
        }

        public void RemoveVolumeProfile(VolumeProfile volumeProfile) {
            if (m_Volume.sharedProfile == volumeProfile) {
                m_Volume.sharedProfile = m_DefaultProfile;
            }
        }

    }

}