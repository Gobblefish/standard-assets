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

    public enum UIEventEnum {
        OnClick,
        OnHover,
        OnDisabled,
        OnIdle,
    }
    
    [System.Serializable]
    public class UIEventParameters {
        public UIEventEnum eventEnum;
        public float scale = 1f;
        public AudioSnippet audioSnippet;
        public Material material;
    }
    
    [CreateAssetMenu(fileName="UIEventController", menuName="UI Event Asset")]
    public class UIEventController : ScriptableObject {

        public UIEventParameters[] eventParameters;

        public void InvokeUIButtonEvent(Gobblefish.UI.Button button, UIEventEnum eventEnum) {
            UIEventParameters eventParams = GetUIEvent(eventEnum);
            if (eventParams == null) { 
                Debug.Log("Trying to run an undefined event.");
                return;
            }
            
            button.transform.localScale = new Vector3(1f, 1f, 1f) * eventParams.scale;
            if (eventParams.audioSnippet != null) {
                eventParams.audioSnippet.Play();
            }
            if (button.image != null && eventParams.material != null) {
                button.image.material = eventParams.material;
            }
            if (button.spriteRenderer != null && eventParams.material != null) {
                button.spriteRenderer.sharedMaterial = eventParams.material;
            }
            
        }

        public void InvokeUISliderEvent(Gobblefish.UI.Slider slider, UIEventEnum eventEnum) {
            UIEventParameters eventParams = GetUIEvent(eventEnum);
            if (eventParams == null) { 
                Debug.Log("Trying to run an undefined event.");
                return;
            }
            
            slider.node.transform.localScale = new Vector3(1f, 1f, 1f) * eventParams.scale;
            if (eventParams.audioSnippet != null) {
                eventParams.audioSnippet.Play();
            }
            if (slider.node.GetComponent<Image>() != null && eventParams.material != null) {
                slider.node.GetComponent<Image>().material = eventParams.material;
            }
            if (slider.node.GetComponent<SpriteRenderer>() != null && eventParams.material != null) {
                slider.node.GetComponent<SpriteRenderer>().sharedMaterial = eventParams.material;
            }
            
        }

        private UIEventParameters GetUIEvent(UIEventEnum eventEnum) {
            for (int i = 0; i < eventParameters.Length; i++) {
                if (eventParameters[i].eventEnum == eventEnum) {
                    return eventParameters[i];
                }
            }
            return null;
        }

    }

}