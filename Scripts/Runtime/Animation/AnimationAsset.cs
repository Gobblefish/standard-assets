// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;

namespace Gobblefish.Animation {

    public class AnimationAsset<TAnimation> : ScriptableObject 
        where TAnimation : class {

        [System.Serializable]
        public class AnimationParams {
            public string name;
            public TAnimation animation;
        }
        
        [SerializeField]
        private AnimationParams[] m_Animations;

        public TAnimation Get(string name) {
            for (int i = 0; i < m_Animations.Length; i++) {
                if (m_Animations[i].name == name) {
                    return m_Animations[i].animation; 
                }
            }
            return null;
        }

        

    }

}
