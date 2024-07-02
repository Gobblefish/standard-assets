// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.UI;

namespace Gobblefish.UI {

    /// <summary>
    /// Formats the ui components of the transform and its children to be consistent
    /// <summary>
    [ExecuteInEditMode]
    public class UIComponentFormatter : Formatter<UIComponent, UIComponentFormat> {

        public override void Format(UIComponent tComponent) {
            Debug.Log("Formatting UI Component " + tComponent.gameObject.name);

            tComponent.ValidateComponents();

            tComponent.SetUIStateResponse(m_Format.active, UIState.Active);
            tComponent.SetUIStateResponse(m_Format.hovered, UIState.Hovered);
            tComponent.SetUIStateResponse(m_Format.idle, UIState.Idle);
            tComponent.SetUIStateResponse(m_Format.disabled, UIState.Disabled);

            Button button = tComponent.GetComponent<Button>();
            if (button != null) {
                button.GetComponent<Image>().sprite = m_Format.buttonBkgSprite;
            }

            Slider slider = tComponent.GetComponent<Slider>();
            if (slider != null) {
                slider.nodeBkg.sprite = m_Format.sliderNodeSprite;
                slider.barBkg.sprite = m_Format.sliderBarSprite;
            }

        }

    }

}