
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using TMPro;

namespace Gobblefish.UI.Text {

    /// <summary>
    /// A scriptable object that can hold the format to be used by a text formatter.
    /// <summary>
    [CreateAssetMenu(fileName="Text Format", menuName="Formats/Text")]
    public class TextFormat : ScriptableFormat {

        // The color of the text.
        public Color textColor;

        // The font the text should use.
        public TMP_FontAsset fontAsset;

        // The spacing between the characters.
        public int characterSpacing;

    }

}