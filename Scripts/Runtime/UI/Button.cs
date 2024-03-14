// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
// Gobblefish
using Gobblefish.Audio;

namespace Gobblefish.UI {

    public class Button : MonoBehaviour, 
        IPointerClickHandler, 
        IPointerEnterHandler,
        IPointerExitHandler {

        // The scale that the selector becomes on hover. 
        [SerializeField]
        private float m_HoverScale = 1.1f;

        // The scale that the selector becomes on hover. 
        [SerializeField]
        private float m_ClickScale = 0.9f;

        // The scale that this selector is by default. 
        private Vector3 m_DefaultScale;

        // The default material for this button.
        private Material m_DefaultMaterial;

        // The duration for which a click is processed.
        public const float CLICK_DURATION = 0.03f;

        [SerializeField]
        private Material m_ClickMaterial;

        [SerializeField]
        private Material m_HoverMaterial;

        [SerializeField]
        private AudioSnippet m_OnClickAudio;

        [SerializeField]
        private AudioSnippet m_OnEnterAudio;

        [SerializeField]
        private AudioSnippet m_OnExitAudio;

        // The event triggered when this button is clicked.
        public UnityEvent m_OnClick = new UnityEvent();

        // The event triggered when this button is clicked.
        public UnityEvent m_OnEnter = new UnityEvent();

        // The event triggered when this button is clicked.
        public UnityEvent m_OnExit = new UnityEvent();

        [SerializeField]
        protected bool m_MouseOver = false;

        // Controls whether the button is currently going through the click cycle.
        private bool m_Clicking = false;

        // The image attached to this component.
        private Image m_Image;

        // The image attached to this component.
        private SpriteRenderer m_SpriteRenderer;

        // Runs once at the before the first frame. 
        void Start() {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_Image = GetComponent<Image>();
            m_DefaultScale = transform.localScale;

            if (m_Image != null) {
                m_DefaultMaterial = m_Image.material;
            }
            if (m_SpriteRenderer != null) {
                m_DefaultMaterial = m_SpriteRenderer.sharedMaterial;
            }

            RectTransform rt = GetComponent<RectTransform>();
            if (rt != null) {
                rt.pivot = new Vector2(0.5f, 0.5f);
            }

            // m_SpriteRenderer.transform.localScale = DEFAULT_SCALE * new Vector3(1f, 1f, 1f);
            // m_SpriteRenderer.sprite = m_IdleSprite;
        }

        void OnEnable() {
            if (m_DefaultMaterial != null) {
                SetState(null, m_DefaultMaterial, 1f);
                m_Clicking = false;
            }
        }

        // Detect if the Cursor clicks on this GameObject
        public void OnPointerClick(PointerEventData pointerEventData) {
            if (pointerEventData.button == PointerEventData.InputButton.Left && !m_Clicking) {
                
                SetState(m_OnClickAudio, m_ClickMaterial, m_ClickScale);
                StartCoroutine(IEClick());
                m_Clicking = true;

                Debug.Log("hello");
            }
        }

        private IEnumerator IEClick() {
            yield return new WaitForSeconds(CLICK_DURATION);
            
            if (m_MouseOver) {
                SetState(null, m_HoverMaterial, m_HoverScale);
            }
            else {
                SetState(null, m_DefaultMaterial, 1f);
            }
            m_Clicking = false;
            m_OnClick.Invoke();

        }

        // Detect if the Cursor starts to pass over the GameObject
        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { return; }

            if (!m_Clicking) {
                SetState(m_OnEnterAudio, m_HoverMaterial, m_HoverScale);
            }
            if (!m_MouseOver) {
                m_OnEnter.Invoke();
            }
            m_MouseOver = true;
        }

        // Detect when Cursor leaves the GameObject
        public void OnPointerExit(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { return; }

            if (!m_Clicking) {
                SetState(m_OnExitAudio, m_DefaultMaterial, 1f);
            }
            if (!m_MouseOver) {
                m_OnExit.Invoke();
            }
            m_MouseOver = false;
        }

        public void SetState(AudioSnippet snippet, Material material, float scale) {
            if (snippet != null) { AudioManager.Sounds.PlaySnippet(snippet); }
            transform.localScale = scale * m_DefaultScale;
            if (m_Image != null && material) {
                m_Image.material = material;
            }
            if (m_SpriteRenderer != null) {
                m_SpriteRenderer.sharedMaterial = material;
            }
        }

    }

}
