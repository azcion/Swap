using UnityEngine;

namespace Assets.Scripts {

	internal sealed class Animation {

		public bool Active;

		private const float Speed = 15f;

		private readonly Transform _item;
		private readonly Vector2 _start;
		private readonly Vector2 _end;

		private float _startTime;
		private float _duration;

		public Animation (Transform item, Vector2 start, Vector2 end) {
			_item = item;
			_start = start;
			_end = end;
		}

		public void Start () {
			Active = true;
			_startTime = Time.time;
			_duration = Vector2.Distance(_start, _end);
		}

		public void Update () {
			if (!Active) {
				return;
			}

			float covered = (Time.time - _startTime) * Speed;
			float fracDist = covered / _duration;
			_item.position = Vector2.Lerp(_start, _end, fracDist);

			if (fracDist >= 1) {
				Active = false;
			}
		}

	}

}