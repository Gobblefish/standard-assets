// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Gobblefish.UI {

    ///<summary>
    /// 
    ///<summary>
    public class Slider : MonoBehaviour, 
        IBeginDragHandler, 
        IDragHandler, 
        IEndDragHandler,
        IPointerDownHandler, 
        IPointerUpHandler,
        IPointerEnterHandler,
        IPointerExitHandler {

        [SerializeField]
        private UIEventController m_UIEventController = null;

        [SerializeField]
        private GameObject m_Node;
        public GameObject node => m_Node;
        
        [SerializeField]
        private RectTransform m_Bar;

        [SerializeField]
        private float m_Value;

        // Controls whether the button is currently going through the click cycle.
        private bool m_Dragging = false;

        // Controls whether the button is currently going through the click cycle.
        private bool m_MouseOver = false;

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent<float> m_OnValueChange = new UnityEvent<float>();

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent m_OnBeginDrag = new UnityEvent();

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent m_OnEndDrag = new UnityEvent();

        //
        private Vector3 m_OriginalScale;
        public Vector3 originalScale => m_OriginalScale;

        void Start() {
            m_OriginalScale = transform.localScale;
        }

        public void SetValue(float value) {
            m_Value = value; 
            SetDraggedPosition(m_Value);
            m_OnValueChange.Invoke(m_Value);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            m_OnBeginDrag.Invoke();
            SetDraggedPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData) {
            SetDraggedPosition(eventData);
        }

        private void SetDraggedPosition(float value) {
            var rt = m_Node.GetComponent<RectTransform>();
            
            Vector3[] v = new Vector3[4];
            m_Bar.GetWorldCorners(v);

            float leftBound = v[0].x;
            float rightBound = v[3].x;
            
            if (value < 0f) {
                value = 0f;
            }
            else if (value > 1f) {
                value = 1f;
            }

            float x = value * (rightBound - leftBound) + leftBound;
            rt.position = new Vector3(x, rt.position.y, rt.position.z);

        }

        private void SetDraggedPosition(PointerEventData eventData) {

            var rt = m_Node.GetComponent<RectTransform>();
            
            Vector3[] v = new Vector3[4];
            m_Bar.GetWorldCorners(v);

            float leftBound = v[0].x;
            float rightBound = v[3].x;
            
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            if (worldPos.x < leftBound) {
                worldPos.x = leftBound;
            }
            else if (worldPos.x > rightBound) {
                worldPos.x = rightBound;
            }

            rt.position = new Vector3(worldPos.x, rt.position.y, rt.position.z);

            m_Value = (worldPos.x - leftBound) / (rightBound - leftBound);
            m_OnValueChange.Invoke(m_Value);

        }

        public void OnEndDrag(PointerEventData eventData) {
            if (m_MouseOver) {
                m_UIEventController.InvokeUISliderEvent(this, UIEventEnum.OnHover);
            }
            else {
                m_UIEventController.InvokeUISliderEvent(this, UIEventEnum.OnIdle);
            }
            m_OnEndDrag.Invoke();
        }

        // Detect if the Cursor clicks on this GameObject
        public void OnPointerDown(PointerEventData pointerEventData) {
            if (pointerEventData.button == PointerEventData.InputButton.Left && !m_Dragging) {
                m_UIEventController.InvokeUISliderEvent(this, UIEventEnum.OnClick);
                m_Dragging = true;
            }
        }

        // Detect if the Cursor clicks on this GameObject
        public void OnPointerUp(PointerEventData pointerEventData) {
            m_Dragging = false;
            if (m_MouseOver) {
                m_UIEventController.InvokeUISliderEvent(this, UIEventEnum.OnHover);
            }
            else {
                m_UIEventController.InvokeUISliderEvent(this, UIEventEnum.OnIdle);
            }
        }

        // Detect if the Cursor starts to pass over the GameObject
        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { return; }

            if (!m_Dragging && !m_MouseOver) {
                m_UIEventController.InvokeUISliderEvent(this, UIEventEnum.OnHover);
            }
            m_MouseOver = true;
        }

        // Detect when Cursor leaves the GameObject
        public void OnPointerExit(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { return; }

            if (!m_Dragging && m_MouseOver) {
                m_UIEventController.InvokeUISliderEvent(this, UIEventEnum.OnIdle);
            }
            m_MouseOver = false;
        }

    }

}

