// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish {

    /// <summary>
    ///
    /// <summary>
    public abstract class ScriptableCollection<Item> : ScriptableObject 
        where Item : class {

        [System.Serializable]
        public class ItemWrapper {
            public string name;
            public Item item;
        }
        
        [SerializeField]
        private ItemWrapper[] collection;

        public void CreateDictionary() {
            Dictionary<string, Item> itemDict = new Dictionary<string, Item>();
            for (int i = 0; i < collection.Length; i++) {
                itemDict.Add(collection[i].name, collection[i].item);
            }
        }

        public Item Get(string name) {
            for (int i = 0; i < collection.Length; i++) {
                if (collection[i].name == name) {
                    return collection[i].item; 
                }
            }
            return null;
        }

    }

}
