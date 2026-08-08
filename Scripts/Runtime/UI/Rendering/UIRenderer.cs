// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.UI {

	[ExecuteInEditMode]
    public abstract class UIRenderer : MonoBehaviour {
		
		public bool render = false;

		private void Update() {
			if (render) {
				Render();
			}
		}

		protected abstract void Render();

    }

}