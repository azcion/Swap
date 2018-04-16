using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	public class PanelController : MonoBehaviour {

		[UsedImplicitly]
		public Sprite[] PanelSprites;

		private static SpriteRenderer _panel0;
		private static SpriteRenderer _panel1;
		private static SpriteRenderer _panel2;
		private static SpriteRenderer _panel3;
		private static SpriteRenderer _panel4;

		public SpriteRenderer GetPanel (int index) {
			switch (index) {
				case 0: return _panel0;
				case 1: return _panel1;
				case 2: return _panel2;
				case 3: return _panel3;
				case 4: return _panel4;
				default: return null;
			}
		}

		private void Debug () {
			_panel0.sprite = PanelSprites[0];
			_panel1.sprite = PanelSprites[1];
			_panel2.sprite = PanelSprites[2];
			_panel3.sprite = PanelSprites[3];
			_panel4.sprite = PanelSprites[4];
		}

		[UsedImplicitly]
		private void OnEnable () {
			Vector3 v = transform.localPosition;
			transform.localPosition = new Vector3(v.x, v.y, Z.Panel);

			_panel0 = transform.GetChild(0).GetComponent<SpriteRenderer>();
			_panel1 = transform.GetChild(1).GetComponent<SpriteRenderer>();
			_panel2 = transform.GetChild(2).GetComponent<SpriteRenderer>();
			_panel3 = transform.GetChild(3).GetComponent<SpriteRenderer>();
			_panel4 = transform.GetChild(4).GetComponent<SpriteRenderer>();
		}

		[UsedImplicitly]
		private void Start () {
			Debug();
		}

	}

}