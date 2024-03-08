// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Events;

namespace Gobblefish {

    // public interface IManager() {
    //     void SetSettings(Settings settings);
    //     void OnGameLoad();
    // }

    ///<summary>
    ///
    ///<summary>
    [DefaultExecutionOrder(-1000)]
    public class GameManager : MonoBehaviour {

        // Singleton.
        public static GameManager INSTANCE;

        [SerializeField]
        private UnityEvent m_OnGameLoad = new UnityEvent();

        [SerializeField]
        private Gobblefish.Audio.AudioManager m_AudioManager;
        public static Gobblefish.Audio.AudioManager Audio => INSTANCE.m_AudioManager;

        [SerializeField]
        private Gobblefish.Graphics.GraphicsManager m_GraphicsManager;
        public static Gobblefish.Graphics.GraphicsManager Graphics => INSTANCE.m_GraphicsManager;

        // Runs once on instantiation.
        protected virtual void Awake() {
            INSTANCE = this;
            m_OnGameLoad.Invoke();
        }

    }
    
}

