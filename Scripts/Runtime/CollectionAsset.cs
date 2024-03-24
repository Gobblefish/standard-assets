// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish {

        public abstract class CollectionAsset<T> : ScriptableObject 
        where T : class {

        [System.Serializable]
        public class TWrapper {
            public string name;
            public T t;
        }
        
        [SerializeField]
        private TWrapper[] tArray;

        public T Get(string name) {
            for (int i = 0; i < tArray.Length; i++) {
                if (tArray[i].name == name) {
                    return tArray[i].t; 
                }
            }
            return null;
        }

    }

}
