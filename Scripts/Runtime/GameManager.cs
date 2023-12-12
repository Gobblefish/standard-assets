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

        // Runs once on instantiation.
        void Awake() {
            INSTANCE = this;
            m_OnGameLoad.Invoke();
        }

        // Validate an array.
        public static bool Validate<T>(T[] array) {
            return array != null && array.Length > 0;
        }

        // Validate a list.
        public static bool Validate<T>(List<T> list) {
            return list != null && list.Count > 0;
        }

    }
    
}

