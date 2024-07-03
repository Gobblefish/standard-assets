// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    /// <summary> 
    /// Use this only when you need a collection of sprites that you want to grab by name.
    /// Not for use with sprites that are randomly interchangeable.
    /// e.g. if you have a "Default" sprite, and you want to grab it just by using the word "Default".
    /// <summary> 
    [CreateAssetMenu(fileName="Sprite Animation Collection", menuName="Collections/SpriteAnims")]
    public class SpriteAnimationCollection : ScriptableCollection<SpriteAnimation> {

    }

}