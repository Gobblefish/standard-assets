// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace GobbleFish.UI {

    using Audio;

    public enum UIState {
        Active,
        Hovered,
        Idle,
        Disabled,
    }
    
    [System.Serializable]
    public class UIStateResponse {

        public float scaleFactor = 1f;
        // public TransformAnimation animation;
        public SoundEffect soundEffect;
        public VisualEffect visualEffect;
        public Material material;

        public void Play(UIComponent uiComponent) {
            uiComponent.transform.localScale = uiComponent.originalScale * scaleFactor;
            
            if (soundEffect != null) {
                soundEffect.Play();
            }

            // if (visualEffect != null) {
            //     visualEffect.Play();
            // }

            Image image = uiComponent.GetComponent<Image>();
            if (image != null) {
                image.material = material;
            } 

        }

    }

    ///<summary>
    ///
    ///<summary>
    public abstract class UIComponent : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
        {

        [HideInInspector]
        protected Vector3 m_OriginalScale = new Vector3(1f, 1f, 1f);
        public Vector3 originalScale => m_OriginalScale;

        [SerializeField]
        protected bool m_IsMouseOver = false;
        public bool isMouseOver => m_IsMouseOver;

        [SerializeField]
        protected bool m_IsActive = false;
        public bool isActive => m_IsActive;

        // [Space(2), Header("Active"), SerializeField]
        [HideInInspector]
        public UIStateResponse m_Active;

        // [Space(2), Header("Hovered"), SerializeField]
        [HideInInspector]
        public UIStateResponse m_Hovered;

        // [Space(2), Header("Idle"), SerializeField]
        [HideInInspector]
        public UIStateResponse m_Idle;

        // [Space(2), Header("Disabled"), SerializeField]
        [HideInInspector]
        public UIStateResponse m_Disabled;
        
        
        public void SetState(UIState stateEnum) {
            UIStateResponse stateResponse = GetUIStateResponse(stateEnum);
            stateResponse.Play(this);
        }

        public void SetUIStateResponse(UIStateResponse newStateResponse, UIState stateEnum) {
            switch (stateEnum) {
                case UIState.Active:
                    m_Active = newStateResponse;
                    break;
                case UIState.Hovered:
                    m_Hovered = newStateResponse;
                    break;
                case UIState.Idle:
                    m_Idle = newStateResponse;
                    break;
                case UIState.Disabled:
                    m_Disabled = newStateResponse;
                    break;
                default:
                    break;
            };
        }

        private UIStateResponse GetUIStateResponse(UIState stateEnum) {
            return stateEnum switch {
                UIState.Active      => m_Active,
                UIState.Hovered     => m_Hovered,
                UIState.Idle        => m_Idle,
                UIState.Disabled    => m_Disabled,
                _                   => null,
            };
        }

        void OnEnable() {
            m_OriginalScale = transform.localScale;
            m_IsActive = false;
            RectTransform rt = GetComponent<RectTransform>();
            if (rt != null) {
                rt.pivot = new Vector2(0.5f, 0.5f);
            }
            SetState(UIState.Idle);
        }

        public void OnPointerClick(PointerEventData pointerEventData) {
            if (pointerEventData.button == PointerEventData.InputButton.Left && !m_IsActive) {
                OnActivate(pointerEventData);
                SetState(UIState.Active);
                m_IsActive = true;
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { 
                return; 
            }
            if (!m_IsActive && !m_IsMouseOver) {
                SetState(UIState.Hovered);
            }
            m_IsMouseOver = true;
        }

        public void OnPointerExit(PointerEventData pointerEventData) {
            if (pointerEventData == null || pointerEventData.pointerEnter == null) { 
                return; 
            }
            if (!m_IsActive && m_IsMouseOver) {
                SetState(UIState.Idle);
            }
            m_IsMouseOver = false;
        }

        protected virtual void OnActivate(PointerEventData pointerEventData) {
            // The core functionality.
        }

        protected void OnRelease() {
            if (m_IsMouseOver) {
                SetState(UIState.Hovered);
            }
            else {
                SetState(UIState.Idle);
            }
            m_IsActive = false;
        }

        public virtual void ValidateComponents() {

        }

    }

}