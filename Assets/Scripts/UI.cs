using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	internal sealed class UI : MonoBehaviour {

		private Transform _gridBackground;

		[UsedImplicitly]
		private void OnEnable () {
			_gridBackground = transform.Find("Grid Background").transform;
		}

		[UsedImplicitly]
		private void Start () {
			_gridBackground.localPosition = Z.VGridBackground;
			_gridBackground.gameObject.SetActive(true);
		}

	}

}