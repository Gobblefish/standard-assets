// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Events;

namespace Gobblefish {

    [DefaultExecutionOrder(-1000)]
    public abstract class Manager<TManager, TSettings> : MonoBehaviour 
        where TManager : Manager<TManager, TSettings>
        where TSettings : Settings<TSettings> {

        // Singleton.
        public static Manager Instance;

        // The settings.
        // public static Settings Settings;

        // The loading function.
        [SerializeField]
        private UnityEvent m_OnLoad = new UnityEvent();

        // Runs once on instantiation.
        void Awake() {
            Instance = this;
            m_OnLoad.Invoke();
        }

    }
    
}

