using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts {

	internal sealed class FieldGenerator : MonoBehaviour {

		public const int Width = 6;
		public const int Height = 5;

		public const int InitPos = 1;
		public const float InitPosHalf = InitPos / 2f;

		private static List<List<Tile>> _tiles;
		private static List<List<bool>> _matched;

		private static List<AnimatePosition> _positionAnimations;
		private static List<AnimateRotation> _rotationAnimations;
		private static List<AnimateScale> _scaleAnimations;

		private static int _animationsPending;

		private List<GameObject> _tilePrefabs;
		
		/// <summary>
		/// Move overlapped tile position to empty spot
		/// </summary>
		public static void Swap (Vector2 start, Vector2 end) {
			int x0 = (int) start.x - 1;
			int y0 = (int) start.y - 1;
			int x1 = (int) end.x - 1;
			int y1 = (int) end.y - 1;

			Tile holdTile = _tiles[y0][x0];

			AnimatePosition a = new AnimatePosition(holdTile, start, end);
			_positionAnimations.Add(a);
			++_animationsPending;

			_tiles[y0][x0] = _tiles[y1][x1];
			_tiles[y1][x1] = holdTile;
		}

		/// <summary>
		/// Call the static function Match.Check and pass it an instance of the grid
		/// </summary>
		public static void Check () {
			_matched = Match.Check(ref _tiles);
		}

		public void Pop () {
			for (int y = 0; y < Height; ++y) {
				for (int x = 0; x < Width; ++x) {
					if (!_matched[y][x]) {
						continue;
					}

					Tile t = _tiles[y][x];

					AnimateRotation aRotation = new AnimateRotation(t);
					_rotationAnimations.Add(aRotation);
					++_animationsPending;

					AnimateScale aScale = new AnimateScale(t);
					_scaleAnimations.Add(aScale);
					++_animationsPending;
				}
			}
		}

		[UsedImplicitly]
		private void OnEnable () {
			_tilePrefabs = new List<GameObject>();
			_tiles = new List<List<Tile>>();
			_positionAnimations = new List<AnimatePosition>();
			_rotationAnimations = new List<AnimateRotation>();
			_scaleAnimations = new List<AnimateScale>();

			foreach (string element in Enum.GetNames(typeof(Element))) {
				_tilePrefabs.Add(Resources.Load("Tile " + element) as GameObject);
			}
		}

		[UsedImplicitly]
		private void Start () {
			for (int y = 0; y < Height; ++y) {
				List<Tile> row = new List<Tile>();

				for (int x = 0; x < Width; ++x) {
					int e = Random.Range(0, _tilePrefabs.Count);

					GameObject go = Instantiate(_tilePrefabs[e], transform);
					go.name = "T " + x + " " + y;

					Transform t = go.transform;

					t.position = new Vector2(
						InitPos + x,
						InitPos + y);
					
					t.GetChild(0).localPosition = Z.VSelectedTileOverlay;
					t.GetChild(1).localPosition = Z.VTileSprite;

					Tile tile = go.AddComponent<Tile>();
					tile.Initialize(go, y, x, (Element) e);

					row.Add(tile);
				}

				_tiles.Add(row);
			}
		}

		[UsedImplicitly]
		private void Update () {
			if (_animationsPending < 1) {
				return;
			}

			for (int i = 0; i < _positionAnimations.Count; ++i) {
				AnimatePosition a = _positionAnimations[i];

				if (!a.Active) {
					_positionAnimations.RemoveAt(i);
					--_animationsPending;

					break;
				}

				a.Update();
			}

			for (int i = 0; i < _rotationAnimations.Count; ++i) {
				AnimateRotation a = _rotationAnimations[i];

				if (!a.Active) {
					_rotationAnimations.RemoveAt(i);
					--_animationsPending;

					break;
				}

				a.Update();
			}

			for (int i = 0; i < _scaleAnimations.Count; ++i) {
				AnimateScale a = _scaleAnimations[i];

				if (!a.Active) {
					_scaleAnimations.RemoveAt(i);
					--_animationsPending;

					break;
				}

				a.Update();
			}
		}

	}

}