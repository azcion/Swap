using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	public class PanelController : MonoBehaviour {

		[UsedImplicitly]
		public Sprite[] PanelSprites = new Sprite[5];

		private static SpriteRenderer[] _panelRenderers;

		public SpriteRenderer GetPanel (int index) {
			return _panelRenderers[index];
		}

		private void Debug () {
			for (int i = 0; i < 5; ++i) {
				_panelRenderers[i].sprite = PanelSprites[i];
			}
		}

		[UsedImplicitly]
		private void OnEnable () {
			Vector3 v = transform.localPosition;
			transform.localPosition = new Vector3(v.x, v.y, Z.Panel);
			_panelRenderers = new SpriteRenderer[PanelSprites.Length];

			for (int i = 0; i < _panelRenderers.Length; ++i) {
				_panelRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
			}
		}

		[UsedImplicitly]
		private void Start () {
			Debug();
		}

	}

}