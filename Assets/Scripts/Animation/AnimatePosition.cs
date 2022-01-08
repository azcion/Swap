using UnityEngine;

namespace Animation {

	internal sealed class AnimatePosition : AbstractAnimation, IAnimate {

		public AnimatePosition (Tile tile, Vector2 start, Vector2 end, float duration = Duration.Medium) {
			Active = true;
			Tile = tile;
			Start = start;
			End = end;
			DurationOverride = duration;
			StartTime = Time.time;
		}

		public AnimatePosition (Tile tile, Vector2 delta, float duration = Duration.Medium)
			: this(tile, new Vector2(tile.Transform.localPosition.x, tile.Transform.localPosition.y),
				new Vector2(tile.Transform.localPosition.x + delta.x, tile.Transform.localPosition.y + delta.y),
				duration) {
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
			Tile.Transform.localPosition = new Vector2(
				Mathf.SmoothStep(Start.x, End.x, Fraction),
				Mathf.SmoothStep(Start.y, End.y, Fraction));

			if (Fraction >= 1) {
				Active = false;
			}
		}

	}

}