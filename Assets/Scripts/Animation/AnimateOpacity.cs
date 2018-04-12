using UnityEngine;

namespace Assets.Scripts.Animation {

	internal sealed class AnimateOpacity : AbstractAnimation, IAnimate {

		private readonly SpriteRenderer _sprite;
		private readonly bool _reverse;

		public AnimateOpacity (Tile tile, bool reverse = false) {
			Active = true;
			Tile = tile;
			_sprite = tile.SpriteTransform.GetComponent<SpriteRenderer>();
			_reverse = reverse;
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

			if (!_reverse) {
				float frac = 1 - covered / Duration.Medium;
				_sprite.color = new Color(1, 1, 1, frac);

				if (frac <= 0) {
					Active = false;
				}
			} else {
				float frac = 0 + covered / Duration.Medium;
				_sprite.color = new Color(1, 1, 1, frac);

				if (frac >= 1) {
					Active = false;
				}
			}
		}

	}

}