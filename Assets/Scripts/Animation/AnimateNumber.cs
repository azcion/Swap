using UnityEngine;
using UnityEngine.UI;

namespace Animation {

	internal sealed class AnimateNumber : AbstractAnimation, IAnimate {

		private readonly Text _text;
		private readonly float _start;
		private readonly float _end;

		public AnimateNumber (Text text, float start, float end, float duration = Duration.Medium) {
			Active = true;
			_text = text;
			_start = start;
			_end = end;
			DurationOverride = duration;
			StartTime = Time.time;
		}

		public bool IsActive () {
			return Active;
		}

		public void Update () {
			if (!Active) {
				return;
			}

			float covered = Time.time - StartTime;
			Fraction = covered / DurationOverride;
			_text.text = ((int) Mathf.SmoothStep(_start, _end, Fraction)).ToString();

			if (Fraction >= 1) {
				Active = false;
			}
		}

	}

}