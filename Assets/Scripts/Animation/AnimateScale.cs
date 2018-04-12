using UnityEngine;

namespace Assets.Scripts.Animation {

	internal sealed class AnimateScale : AbstractAnimation, IAnimate {

		public AnimateScale (Tile tile, float duration = Duration.Medium) {
			Active = true;
			Tile = tile;
			Start = tile.Transform.localScale;
			End = new Vector3(.5f, .5f, 1);
			StartTime = Time.time;
			DurationOverride = duration;
		}

		public bool IsActive () {
			return Active;
		}

		public void Update () {
			if (!Active) {
				return;
			}

			float covered = Time.time - StartTime;
			float frac = covered / DurationOverride;
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