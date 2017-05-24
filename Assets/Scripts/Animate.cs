using UnityEngine;

namespace Assets.Scripts {

	internal abstract class Animate {

		protected const float DurationShort = .2f;
		protected const float DurationMedium = .4f;
		protected const float DurationLong = .6f;

		public bool Active;

		protected Tile Tile;
		protected float StartTime;
		protected float Fraction;
		protected Vector3 Start;
		protected Vector3 End;

		public abstract void Update ();

	}

}