// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish {

    /// <summary>
    ///
    /// <summary>
    public abstract class ScriptableCollection<Item> : ScriptableObject 
    where Item : class {

        // A constant threshold beyond which we use a dictionary.
        public const int SEARCH_THRESHOLD = 20;

        // A dictionary in case the collection is large.
        public Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

        [System.Serializable]
        public class ItemWrapper {
            public string name;
            public Item item;
        }
        
        [SerializeField] // This should be private, and got through an iterable.
        public ItemWrapper[] collection;

        public void CreateDictionary() {
            Dictionary<string, Item> itemDict = new Dictionary<string, Item>();
            for (int i = 0; i < collection.Length; i++) {
                itemDict.Add(collection[i].name, collection[i].item);
            }
        }

        public Item Get(string name) {
            bool useDict = 
                collection.Length >= SEARCH_THRESHOLD 
                && itemDict != null 
                && itemDict.Count > 0
                && itemDict.ContainsKey(name);
            if (useDict) {
                return itemDict[name];
            }

            for (int i = 0; i < collection.Length; i++) {
                if (collection[i].name == name) {
                    return collection[i].item; 
                }
            }
            return null;
        }

        // public IEnumerator<Item> GetEnumerator() {
        //     return collection.GetEnumerator().item;
        // }

    }

}
