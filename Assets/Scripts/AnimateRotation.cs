using UnityEngine;

namespace Assets.Scripts {

	internal sealed class AnimateRotation {

		public bool Active;

		private const float Duration = .2f;

		private readonly Transform _tile;

		private float _frac;

		public AnimateRotation (Transform tile) {
			_tile = tile;
		}

		public void Start () {
			Active = true;
		}

		public void Update () {
			if (!Active) {
				return;
			}

			_frac += Time.deltaTime / Duration;
			_tile.rotation = Quaternion.Euler(0, 0, Mathf.SmoothStep(0, 360, _frac));

			if (_frac >= 1) {
				Active = false;
			}
		}

	}

}