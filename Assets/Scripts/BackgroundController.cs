using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	public class BackgroundController : MonoBehaviour {

		[UsedImplicitly]
		private void OnEnable () {
			transform.localPosition = Z.VBackground;
		}

	}

}