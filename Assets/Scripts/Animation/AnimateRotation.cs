using UnityEngine;

namespace Assets.Scripts.Animation {

	internal sealed class AnimateRotation : AbstractAnimation, IAnimate {

		public AnimateRotation (Tile tile) {
			Active = true;
			Tile = tile;
		}

		public bool IsActive () {
			return Active;
		}

		public void Update () {
			if (!Active) {
				return;
			}

			Fraction += Time.deltaTime / DurationMedium;
			Tile.Transform.rotation = Quaternion.Euler(0, 0, Mathf.SmoothStep(0, 360, Fraction));

			if (Fraction >= 1) {
				Active = false;
			}
		}

	}

}