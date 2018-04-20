using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {

	internal sealed class Panel {

		public readonly SpriteRenderer Renderer;
		public readonly Text Text;

		public readonly Element Type;
		public readonly int Health;

		public int Value { get; private set; }

		public Panel (Transform transform, Element type, int health) {
			Renderer = transform.GetComponent<SpriteRenderer>();
			Text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
			Type = type;
			Health = health;
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

}