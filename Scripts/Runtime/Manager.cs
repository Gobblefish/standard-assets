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
        where TSettings : Settings<TSettings> 
    {

        // Singleton.
        public static TManager Instance;

        // The settings.
        public TSettings m_Settings;
        public static TSettings Settings => Instance.m_Settings;

        // The loading function.
        [SerializeField]
        private UnityEvent m_OnLoad = new UnityEvent();

        // Runs once on instantiation.
        protected virtual void Awake() {
            Instance = this.GetComponent<TManager>();
            m_Settings = m_Settings.Read();
            m_OnLoad.Invoke();
        }

    }
    
}