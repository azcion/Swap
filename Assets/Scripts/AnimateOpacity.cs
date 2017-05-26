using UnityEngine;

namespace Assets.Scripts {

	internal sealed class AnimateOpacity : Animate, IAnimate {

		private readonly SpriteRenderer _sprite;

		public AnimateOpacity (Tile tile) {
			Active = true;
			Tile = tile;
			_sprite = tile.SpriteTransform.GetComponent<SpriteRenderer>();
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
			float frac = 1 - covered / DurationMedium;
			_sprite.color = new Color(1, 1, 1, frac);

			if (frac <= 0) {
				Active = false;
			}
		}

	}

}