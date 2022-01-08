using UnityEngine;
using UnityEngine.UI;

internal sealed class Panel {

	public readonly SpriteRenderer Renderer;
	public readonly Text Text;

	public readonly Element Type;
	public readonly int Health;

	public int Value { get; private set; }

	private static readonly Color[] TextColor;

	static Panel () {
		TextColor = new[] {
			Color.black,
			Color.magenta,
			new Color(180/255f, 225/255f, 240/255f), // Water 
			new Color(240/255f, 145/255f,  90/255f), // Fire
			new Color(195/255f, 250/255f, 160/255f), // Leaf
			new Color(195/255f, 200/255f, 210/255f), // Rock 
			new Color(255/255f, 255/255f, 180/255f), // Wind
		};
	}

	public Panel (int index, Transform transform, Element type, int health) {
		Renderer = transform.GetComponent<SpriteRenderer>();
		Type = type;
		Health = health;

		Text = ObjectHooks.PanelTextComponents[index];
		//Align UI to world point
		Text.transform.position = CameraController.Main.WorldToScreenPoint(transform.position);
		Text.color = TextColor[(int) type];
	}

	public void Update () {
		Text.text = Value.ToString();
	}

	public void AddValue (int value) {
		Value += value;
	}

	public void ResetValue () {
		Value = 0;
	}

}