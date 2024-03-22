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
        IEndDragHandler {

        [SerializeField]
        private UIEventController m_UIEventController = null;

        [SerializeField]
        private GameObject m_Node;
        public GameObject node => node;
        private Vector3 m_NodeDefaultScale;
        public Vector3 nodeDefaultScale => m_NodeDefaultScale;
        
        [SerializeField]
        private RectTransform m_Bar;

        [SerializeField]
        private float m_Value;

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent<float> m_OnValueChange = new UnityEvent<float>();

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent m_OnBeginDrag = new UnityEvent();

        // The event to trigger when the value is changed.
        [SerializeField]
        private UnityEvent m_OnEndDrag = new UnityEvent();

        void Start() {
            m_NodeDefaultScale = m_Node.transform.localScale;
        }

        public void SetValue(float value) {
            m_Value = value; 
            SetDraggedPosition(m_Value);
            m_OnValueChange.Invoke(m_Value);
        }

        public void OnBeginDrag(PointerEventData eventData) {
            m_OnBeginDrag.Invoke();
            m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnClick);
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
            m_UIEventController.InvokeUIEvent(this, UIEventEnum.OnIdle);
            m_OnEndDrag.Invoke();
        }

    }

}

