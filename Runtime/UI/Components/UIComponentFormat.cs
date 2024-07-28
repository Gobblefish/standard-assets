// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.UI {

    [CreateAssetMenu(fileName="UI Component Format", menuName="Formats/UI Component")]
    public class UIComponentFormat : ScriptableFormat {

        [Space(2), Header("Sprites")]

        public Sprite buttonBkgSprite;

        public Sprite sliderNodeSprite;

        public Sprite sliderBarSprite;
        
        [Space(2), Header("States")]

        public UIStateResponse active;

        public UIStateResponse hovered;

        public UIStateResponse idle;

        public UIStateResponse disabled;

    }

}