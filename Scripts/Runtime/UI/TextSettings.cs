
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using TMPro;

namespace Blobbers.Graphics {

    [CreateAssetMenu(fileName="TextSettings", menuName="Text/Settings")]
    public class TextSettings : ScriptableObject {

        public Color textColor;
        public TMP_FontAsset fontAsset;
        public bool fontAutoSize;
        public int fontSizeMin;
        public int fontSizeMax;
        public int characterSpacing;

    }

}