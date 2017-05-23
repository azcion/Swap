using UnityEngine;

namespace Assets.Scripts {

	internal sealed class AnimateRotation {

		public bool Active;

		private const float Speed = 15f;

		private readonly Transform _item;
		private readonly float _duration;

		private float _startTime;

		public AnimateRotation (Transform item, float duration) {
			_item = item;
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
			_item.Rotate(Vector3.back * Speed);

			if (frac >= 1) {
				Active = false;
			}
		}

		public Transform GetTransform () {
			return _item;
		}

	}

}