using UnityEngine;

namespace Assets.Scripts {

	public class AnimateScale {

		public bool Active;

		private const float Speed = 10f;

		private readonly Transform _item;
		private readonly Vector3 _start;
		private readonly Vector3 _scale;
		private readonly float _duration;

		private float _startTime;

		public AnimateScale (Transform item, Vector3 scale, float duration) {
			_item = item;
			_start = item.transform.localScale;
			_scale = scale;
			_duration = duration;
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
			float frac = covered / _duration;
			_item.localScale = Vector3.Lerp(_start, _scale, frac);

			if (frac >= 1) {
				Active = false;
			}
		}

	}

}