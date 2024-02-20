// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Events;

namespace Gobblefish {

    public abstract class Settings<TSettings> where TSettings : Settings<TSettings> {

        public string FilePath => "settings/" + typeof(TSettings).Name;

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }

    }
    
}

