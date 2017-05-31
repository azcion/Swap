using UnityEngine;

namespace Assets.Scripts.Animation {

	internal sealed class AnimatePosition : AbstractAnimation, IAnimate {

		public AnimatePosition (Tile tile, Vector2 start, Vector2 end) {
			Active = true;
			Tile = tile;
			Start = start;
			End = end;
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
			Fraction = covered / DurationShort;
			Tile.Transform.position = new Vector2(
				Mathf.SmoothStep(Start.x, End.x, Fraction),
				Mathf.SmoothStep(Start.y, End.y, Fraction));

			if (Fraction >= 1) {
				Active = false;
			}
		}

	}

}