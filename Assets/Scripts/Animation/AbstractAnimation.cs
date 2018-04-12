using UnityEngine;

namespace Assets.Scripts.Animation {

	internal abstract class AbstractAnimation {

		protected bool Active;
		protected Tile Tile;
		protected float StartTime;
		protected float Fraction;
		protected Vector3 Start;
		protected Vector3 End;
		protected float DurationOverride;

	}

}