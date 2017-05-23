using UnityEngine;

namespace Assets.Scripts {

	internal sealed class AnimatePosition {

		public bool Active;

		private const float Duration = .2f;

		private readonly Transform _tile;
		private readonly Vector2 _start;
		private readonly Vector2 _end;

		private float _startTime;
		private float _frac;

		public AnimatePosition (Transform tile, Vector2 start, Vector2 end) {
			_tile = tile;
			_start = start;
			_end = end;
		}

		public void Start () {
			Active = true;
			_startTime = Time.time;		}

		public void Update () {
			if (!Active) {
				return;
			}

			_frac += Duration - (Time.time - _startTime);
			_tile.position = new Vector2(
				Mathf.SmoothStep(_start.x, _end.x, _frac),
				Mathf.SmoothStep(_start.y, _end.y, _frac));

			if (_frac >= 1) {
				Active = false;
			}
		}

	}

}