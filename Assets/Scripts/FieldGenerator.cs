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
		private static List<IAnimate> _animations;

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

			IAnimate a = new AnimatePosition(holdTile, start, end);
			_animations.Add(a);
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

		/// <summary>
		/// Add removal animations of matched tiles
		/// </summary>
		public static void Pop () {
			for (int y = 0; y < Height; ++y) {
				for (int x = 0; x < Width; ++x) {
					if (!_matched[y][x]) {
						continue;
					}

					Tile t = _tiles[y][x];

					_animations.Add(new AnimateRotation(t));
					_animations.Add(new AnimateScale(t));
					_animations.Add(new AnimateOpacity(t));
					_animationsPending += 3;
				}
			}
		}

		/// <summary>
		/// Call Update methods of animations and remove them if needed
		/// </summary>
		private static void Animate (IList<IAnimate> animations) {
			List<int> indexesToRemove = new List<int>();

			for (int i = 0; i < animations.Count; ++i) {
				IAnimate a = animations[i];

				if (!a.IsActive()) {
					indexesToRemove.Add(i);
					continue;
				}

				a.Update();
			}

			for (int i = 0; i < indexesToRemove.Count; ++i) {
				animations.RemoveAt(indexesToRemove[i] - i);
			}

			_animationsPending -= indexesToRemove.Count;
		}

		[UsedImplicitly]
		private void OnEnable () {
			_tilePrefabs = new List<GameObject>();
			_tiles = new List<List<Tile>>();
			_animations = new List<IAnimate>();

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

			Animate(_animations);
		}

	}

}