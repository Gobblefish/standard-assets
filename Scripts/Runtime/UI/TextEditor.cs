// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using TMPro;

namespace Blobbers.UI {

    [ExecuteInEditMode]
    public class TextEditor : MonoBehaviour {

        [SerializeField]
        private TextSettings m_TextSettings;

        void Start() {
            if (Application.isPlaying && m_TextSettings != null) {
                EditText();
            }
        }

        void Update() {
            if (!Application.isPlaying && m_TextSettings != null) {
                EditText();
            }
        }

        private void EditText() {

            TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
            if (textMesh != null) {
                textMesh.color = m_TextSettings.textColor;
                textMesh.font = m_TextSettings.fontAsset;
                textMesh.enableAutoSizing = m_TextSettings.fontAutoSize;
                textMesh.fontSizeMin = m_TextSettings.fontSizeMin;
                textMesh.fontSizeMax = m_TextSettings.fontSizeMax;
                textMesh.characterSpacing = m_TextSettings.characterSpacing;
            }
            
            List<TextMeshProUGUI> textMeshes = new List<TextMeshProUGUI>();
            recursive_CollectTextMeshesUnder(transform, ref textMeshes);
            
            foreach (TextMeshProUGUI tMesh in textMeshes) {
                tMesh.color = m_TextSettings.textColor;
                tMesh.font = m_TextSettings.fontAsset;
                tMesh.enableAutoSizing = m_TextSettings.fontAutoSize;
                tMesh.fontSizeMin = m_TextSettings.fontSizeMin;
                tMesh.fontSizeMax = m_TextSettings.fontSizeMax;
                tMesh.characterSpacing = m_TextSettings.characterSpacing;
            }

        }

        public void recursive_CollectTextMeshesUnder(Transform transform, ref List<TextMeshProUGUI> textMeshList) {
            if (transform.childCount == 0) { return; }

            foreach (Transform child in transform)  {

                TextMeshProUGUI tmpPro = child.GetComponent<TextMeshProUGUI>();
                TextEditor textEditor = child.GetComponent<TextEditor>();

                if (tmpPro != null && !textMeshList.Contains(tmpPro)) {
                    textMeshList.Add(tmpPro);
                }

                // call recursively unless its overridden by another text editor.
                if (!textEditor) {
                    recursive_CollectTextMeshesUnder(child, ref textMeshList);
                }

            }

        }

    }

}