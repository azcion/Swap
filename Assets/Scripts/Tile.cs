using UnityEngine;

namespace Assets.Scripts {

	internal sealed class Tile : MonoBehaviour {

		public int Y { get; private set; }
		public int X { get; private set; }

		public Element Type { get; private set; }

		public GameObject GO { get; private set; }
		public Transform Transform { get; private set; }
		public Vector3 Position { get; private set; }

		public GameObject OverlayGO { get; private set; }
		public Transform OverlayTransform { get; private set; }
		public Vector3 OverlayPosition { get; private set; }

		public GameObject SpriteGO { get; private set; }
		public Transform SpriteTransform { get; private set; }
		public Vector3 SpritePosition { get; private set; }

		public Tile Initialize (GameObject go, int y, int x, Element type) {
			Y = y;
			X = x;
			Type = type;

			GO = go;
			Transform = GO.transform;
			Position = Transform.localPosition;
			
			OverlayTransform = Transform.Find("Selected Overlay");
			OverlayGO = OverlayTransform.gameObject;
			OverlayPosition = OverlayTransform.localPosition;

			SpriteTransform = Transform.Find("Sprite");
			SpriteGO = SpriteTransform.gameObject;
			SpritePosition = SpriteTransform.localPosition;

			return this;
		}

	}

}