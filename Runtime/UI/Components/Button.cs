// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GobbleFish.UI {

    public class Button : UIComponent {

        // The duration for which a click is processed.
        public const float ACTIVATION_DELAY = 0.03f;

        // The event triggered when this button is clicked.
        public UnityEvent m_OnClick = new UnityEvent();

        public override void ValidateComponents() {
            this.Require<Image>();
        }

        protected override void OnActivate(PointerEventData pointerEventData) {
            StartCoroutine(IEActivate());
            m_OnClick.Invoke();
        }

        // Triggers the actual click event a short duration after the actual click.
        private IEnumerator IEActivate() {
            yield return new WaitForSeconds(ACTIVATION_DELAY);
            
            // Resets the state after clicked.
            OnRelease();
            m_OnClick.Invoke();

        }

    }

}
