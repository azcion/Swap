using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	internal sealed class GridBackgroundController : MonoBehaviour {

		[UsedImplicitly]
		private void OnEnable () {
			transform.localPosition = Z.VGridBackground;
		}

	}

}