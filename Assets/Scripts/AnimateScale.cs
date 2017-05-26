using UnityEngine;

namespace Assets.Scripts {

	internal sealed class AnimateScale : Animate, IAnimate {

		public AnimateScale (Tile tile) {
			Active = true;
			Tile = tile;
			Start = tile.Transform.localScale;
			End = new Vector3(.5f, .5f, 1);
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
			float frac = covered / DurationMedium;
			Tile.Transform.localScale = new Vector3(
				Mathf.SmoothStep(Start.x, End.x, frac),
				Mathf.SmoothStep(Start.y, End.y, frac),
				1);

			if (frac >= 1) {
				Active = false;
			}
		}

	}

}