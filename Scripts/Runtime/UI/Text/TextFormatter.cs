// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.UI.Text {

    using TMPro;

    /// <summary>
    /// Formats the text meshes of the transform and its children to be consistent
    /// <summary>
    [ExecuteInEditMode]
    public class TextFormatter : Formatter<TextMeshProUGUI, TextFormat> {

        public override void Format(TextMeshProUGUI tComponent) {
            // Set the color and font.
            tComponent.color = m_Format.textColor;
            tComponent.font = m_Format.fontAsset;

            // Auto size the text to the size of rect.
            tComponent.enableAutoSizing = true;
            tComponent.fontSizeMin = 0;
            tComponent.fontSizeMax = (int)1e5f;

            // Set the character spacing.
            tComponent.characterSpacing = m_Format.characterSpacing;
        }

    }

}