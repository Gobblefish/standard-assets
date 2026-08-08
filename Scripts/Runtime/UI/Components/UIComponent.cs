// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.UI {

	[ExecuteInEditMode]
    public abstract class UIComponent : MonoBehaviour {

		private void Update() {
			print("HI");
		}

		public void Add<TUIComponent>() where TUIComponent : UIComponent {
			
		}

    }

}