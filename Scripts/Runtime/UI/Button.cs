// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Gobblefish.UI {

    public class Button : MonoBehaviour, 
        IPointerClickHandler, 
        IPointerEnterHandler,
        IPointerExitHandler {

        [SerializeField]
        private UIEventController m_UIEventController = null;

        [SerializeField]
        protected bool m_MouseOver = false;

        // The duration for which a click is processed.
        public const float CLICK_DURATION = 0.03f;

        // The scale that this selector is by default. 
        private Vector3 m_DefaultScale;
        public Vector3 defaultScale => m_DefaultScale;

        // The default material for this button.
        private Material m_DefaultMaterial;
        public Material defaultMaterial => m_DefaultMaterial;

        // The event triggered when this button is clicked.
        public UnityEvent m_OnClick = new UnityEvent();

        // The event triggered when this button is clicked.
        public UnityEvent m_OnEnter = new UnityEvent();

        // The event triggered when this button is clicked.
        public UnityEvent m_OnExit = new UnityEvent();

        // Controls whether the button is currently going through the click cycle.
        private bool m_Clicking = false;

        // The image attached to this component.
        private Image m_Image;
        public Image image => m_Image;

        // The image attached to this component.
        private SpriteRenderer m_SpriteRenderer;
        public SpriteRenderer spriteRenderer => m_SpriteRenderer;

        // Runs once at the before the first frame. 
        void Start() {
            // Get all the default parameters.
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
        }

        void OnEnable() {
            if (m_DefaultMaterial != null) {
                m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnIdle);
                m_Clicking = false;
            }
        }

        // Detect if the Cursor clicks on this GameObject
        public void OnPointerClick(PointerEventData pointerEventData) {
            if (pointerEventData.button == PointerEventData.InputButton.Left && !m_Clicking) {
                
                m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnClick);
                StartCoroutine(IEClick());
                m_Clicking = true;

                Debug.Log("hello");
            }
        }

        // Triggers the actual click event a short duration after the actual click.
        private IEnumerator IEClick() {
            yield return new WaitForSeconds(CLICK_DURATION);
            
            if (m_MouseOver) {
                m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnHover);
            }
            else {
                m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnIdle);
            }
            m_Clicking = false;
            m_OnClick.Invoke();

        }

        // Detect if the Cursor starts to pass over the GameObject
        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { return; }

            if (!m_Clicking && !m_MouseOver) {
                m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnHover);
            }
            if (!m_MouseOver) {
                m_OnEnter.Invoke();
            }
            m_MouseOver = true;
        }

        // Detect when Cursor leaves the GameObject
        public void OnPointerExit(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { return; }

            if (!m_Clicking && m_MouseOver) {
                m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnIdle);
            }
            if (m_MouseOver) {
                m_OnExit.Invoke();
            }
            m_MouseOver = false;
        }

    }

}
