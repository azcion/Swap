using JetBrains.Annotations;
using UnityEngine;

[UsedImplicitly]
internal sealed class Tile : MonoBehaviour {

	public int Y { get; private set; }
	public int X { get; private set; }

	public Element Type { get; private set; }
	public bool IsEmpty { get; private set; }

	public GameObject GO { get; private set; }
	public Transform Transform { get; private set; }
	public Vector3 Position { get; private set; }

	public GameObject OverlayGO { get; private set; }
	public Transform OverlayTransform { get; private set; }
	public Vector3 OverlayPosition { get; private set; }

	public GameObject SpriteGO { get; private set; }
	public Transform SpriteTransform { get; private set; }
	public Vector3 SpritePosition { get; private set; }

	public void Initialize (GameObject go, int y, int x, Element type) {
		Y = y;
		X = x;
		Type = type;
		IsEmpty = false;

		GO = go;
		Transform = GO.transform;
		Position = Transform.localPosition;
			
		OverlayTransform = Transform.Find("Selected Overlay");
		OverlayGO = OverlayTransform.gameObject;
		OverlayPosition = OverlayTransform.localPosition;

		SpriteTransform = Transform.Find("Sprite");
		SpriteGO = SpriteTransform.gameObject;
		SpritePosition = SpriteTransform.localPosition;
	}

	public void SetEmpty () {
		if (IsEmpty) {
			Debug.LogError("Tile is already empty.");
		}

		IsEmpty = true;
	}

	public void Destroy () {
		Destroy(OverlayGO, .65f);
		Destroy(SpriteGO, .65f);
		Destroy(GO, .65f);
		Destroy(this, .65f);
	}

}