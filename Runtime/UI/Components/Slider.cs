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
    public class Slider : UIComponent, 
        IDragHandler, 
        IPointerUpHandler
        {

        [SerializeField]
        private GameObject m_Node;
        public Image nodeBkg => m_Node.GetComponent<Image>();
        
        [SerializeField]
        private RectTransform m_Bar;
        public Image barBkg => m_Bar.GetComponent<Image>(); 

        [SerializeField]
        private float m_Value;

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent<float> m_OnValueChange = new UnityEvent<float>();

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent m_OnEndDrag = new UnityEvent();

        public void SetValue(float value) {
            SetNodePositionToValue(m_Value);
            m_OnValueChange.Invoke(m_Value);
        }

        protected override void OnActivate(PointerEventData pointerEventData) {
            SetValueToMousePosition(pointerEventData);
        }

        public void OnDrag(PointerEventData pointerEventData) {
            SetValueToMousePosition(pointerEventData);
        }

        public void OnPointerUp(PointerEventData pointerEventData) {
            if (m_IsActive) {
                m_OnValueChange.Invoke(m_Value);
            }
            OnRelease();
        }

        private void SetNodePositionToValue(float value) {
            RectTransform nodeRT = m_Node.GetComponent<RectTransform>();
            
            // Get the bounds of the bar.
            Vector3[] v = new Vector3[4];
            m_Bar.GetWorldCorners(v);
            float leftBound = v[0].x;
            float rightBound = v[3].x;
            
            // Clamp the value between 0 and 1.
            if (value < 0f) {
                value = 0f;
            }
            else if (value > 1f) {
                value = 1f;
            }

            // Scale and set the position of the node / cached value based on the given value.
            float xPosition = value * (rightBound - leftBound) + leftBound;
            nodeRT.position = new Vector3(xPosition, nodeRT.position.y, nodeRT.position.z);
            m_Value = value;

        }

        private void SetValueToMousePosition(PointerEventData eventData) {
            RectTransform nodeRT = m_Node.GetComponent<RectTransform>();
            
            // Get the bounds of the bar.
            Vector3[] v = new Vector3[4];
            m_Bar.GetWorldCorners(v);
            float leftBound = v[0].x;
            float rightBound = v[3].x;
            
            // Get the position of the mouse along the bar.
            float xPosition = Camera.main.ScreenToWorldPoint(eventData.position).x;
            if (xPosition < leftBound) {
                xPosition = leftBound;
            }
            else if (xPosition > rightBound) {
                xPosition = rightBound;
            }

            // Set the position and value of the node. 
            nodeRT.position = new Vector3(xPosition, nodeRT.position.y, nodeRT.position.z);
            m_Value = (xPosition - leftBound) / (rightBound - leftBound);

            // Invoke the on value changed event.
            m_OnValueChange.Invoke(m_Value);

        }

    }

}

