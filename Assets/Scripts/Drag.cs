using Assets.Scripts.Animation;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {

	internal sealed class Drag : MonoBehaviour {

		private struct Bounds {

			/// <summary> Reduce bounding box size by value on each side </summary>
			private const float Intolerance = .45f;

			internal const float X0 = FieldGenerator.InitPosHalf + Intolerance;
			internal const float Y0 = FieldGenerator.InitPosHalf + Intolerance;
			internal const float X1 = FieldGenerator.InitPosHalf + FieldGenerator.Width - Intolerance;
			internal const float Y1 = FieldGenerator.InitPosHalf + FieldGenerator.Height - Intolerance;

		}

		public static bool AllowUnlock;
		public static bool Lock;

		private Transform _parent;
		private GameObject _selectedOverlay;
		private Transform _sprite;
		private Vector3 _initialPos;
		private Vector3 _hoverOverPos;
		private Vector3 _mOld;

		private float _timeRemaining;

		/// <summary> 
		/// Snap tile position to grid 
		/// </summary>
		private static Vector2 Interpolate (Vector2 v) {
			int x = Mathf.RoundToInt(v.x);
			int y = Mathf.RoundToInt(v.y);

			return new Vector2(x, y);
		}

		[UsedImplicitly]
		private void OnEnable () {
			Lock = false;
			_parent = transform.parent.transform;
			_selectedOverlay = transform.parent.GetChild(0).gameObject;
			_sprite = transform.parent.GetChild(1);
		}

		[UsedImplicitly]
		private void LateUpdate () {
			if (AllowUnlock && Animate.TilesPoppedThisRound == 0 && !Animate.IsAnimating) {
				Lock = false;
			}

			_timeRemaining -= Time.deltaTime;

			Text T = GameObject.Find("Text").transform.GetComponent<Text>();
			T.text = Lock + "";
		}

		[UsedImplicitly]
		private void OnMouseDown () {
			if (Lock) {
				return;
			}

			// Bring to front to prevent from being overlapped
			_sprite.localPosition = Z.VSelectedTileSprite;
			_selectedOverlay.SetActive(true);
			_initialPos = Interpolate(_parent.position);
			_hoverOverPos = _initialPos;
			_timeRemaining = Duration.DragTime;
		}

		[UsedImplicitly]
		private void OnMouseUp () {
			if (Lock) {
				return;
			}

			Lock = true;
			AllowUnlock = false;
			_timeRemaining = 0;
			_selectedOverlay.SetActive(false);
			// Return to initial Z
			_sprite.localPosition = Z.VTileSprite;
			_parent.position = Interpolate(_parent.position);

			FieldGenerator.Check();
		}

		[UsedImplicitly]
		private void OnMouseDrag () {
			if (Lock) {
				return;
			}

			if (_timeRemaining <= 0) {
				OnMouseUp();
				return;
			}

			Vector3 m = Input.mousePosition;

			if ((int) _mOld.x == (int) m.x && (int) _mOld.y == (int) m.y) {
				return;
			}

			_mOld = m;
			m = Camera.main.ScreenToWorldPoint(m);

			float x = m.x;
			float y = m.y;

			if (x < Bounds.X0) {
				x = Bounds.X0;
			} else if (x > Bounds.X1) {
				x = Bounds.X1;
			}

			if (y < Bounds.Y0) {
				y = Bounds.Y0;
			} else if (y > Bounds.Y1) {
				y = Bounds.Y1;
			}

			Vector2 pos = new Vector2(x, y);
			_hoverOverPos = Interpolate(pos);
			_parent.position = pos;

			if (_hoverOverPos == _initialPos) {
				return;
			}

			// If diagonal movement was made without registering corner tiles
			if ((int) _hoverOverPos.x != (int) _initialPos.x && (int) _hoverOverPos.y != (int) _initialPos.y) {
				// Skipped tile
				FieldGenerator.Swap(new Vector2(_initialPos.x, _hoverOverPos.y), _initialPos);

				// Tile diagonal to the empty spot
				FieldGenerator.Swap(_hoverOverPos, new Vector2(_initialPos.x, _hoverOverPos.y));
			} else {
				FieldGenerator.Swap(_hoverOverPos, _initialPos);
			}

			_initialPos = _hoverOverPos;
		}

	}

}