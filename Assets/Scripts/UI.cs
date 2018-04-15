using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	internal sealed class UI : MonoBehaviour {

		[UsedImplicitly]
		public Sprite[] PanelSprites;

		private Transform _gridBackground;
		private Transform _panels;

		private SpriteRenderer _panel0;
		private SpriteRenderer _panel1;
		private SpriteRenderer _panel2;
		private SpriteRenderer _panel3;
		private SpriteRenderer _panel4;

		private void Debug () {
			_panel0.sprite = PanelSprites[0];
			_panel1.sprite = PanelSprites[1];
			_panel2.sprite = PanelSprites[2];
			_panel3.sprite = PanelSprites[3];
			_panel4.sprite = PanelSprites[4];
		}

		[UsedImplicitly]
		private void OnEnable () {
			_gridBackground = transform.Find("Grid Background").transform;
			_panels = transform.Find("Panels").transform;
			_panel0 = _panels.GetChild(0).GetComponent<SpriteRenderer>();
			_panel1 = _panels.GetChild(1).GetComponent<SpriteRenderer>();
			_panel2 = _panels.GetChild(2).GetComponent<SpriteRenderer>();
			_panel3 = _panels.GetChild(3).GetComponent<SpriteRenderer>();
			_panel4 = _panels.GetChild(4).GetComponent<SpriteRenderer>();
		}

		[UsedImplicitly]
		private void Start () {
			_gridBackground.localPosition = Z.VGridBackground;
			_gridBackground.gameObject.SetActive(true);

			Debug();
		}

	}

}