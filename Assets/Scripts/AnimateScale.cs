using UnityEngine;

namespace Assets.Scripts {

	internal sealed class AnimateScale {

		public bool Active;

		private const float Duration = .2f;

		private static readonly Vector3 End = new Vector3(.5f, .5f, 1);

		private readonly Transform _tile;
		private readonly Vector3 _start;

		private float _startTime;

		public AnimateScale (Transform tile) {
			_tile = tile;
			_start = tile.transform.localScale;
		}

		public void Start () {
			Active = true;
			_startTime = Time.time;
		}

		public void Update () {
			if (!Active) {
				return;
			}

			float covered = Time.time - _startTime;
			float frac = covered / Duration;
			_tile.localScale = new Vector3(
				Mathf.SmoothStep(_start.x, End.x, frac),
				Mathf.SmoothStep(_start.y, End.y, frac),
				1);

			if (frac >= 1) {
				Active = false;
			}
		}

	}

}