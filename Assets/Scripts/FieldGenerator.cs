using System;
using System.Collections.Generic;
using Assets.Scripts.Animation;
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

		private List<GameObject> _tilePrefabs;
		
		/// <summary>
		/// Move overlapped tile position to empty spot
		/// </summary>
		public static void Swap (Vector2 start, Vector2 end) {
			SwapPos((int) start.x - 1, (int) start.y - 1, (int) end.x - 1, (int) end.y - 1);
		}

		/// <summary>
		/// Call the static function Match.Check and pass it an instance of the grid
		/// </summary>
		public static void Check () {
			List<List<Element>> matched = Match.Check(ref _tiles);
			Animate.Pop(_tiles, matched);
			Drop();
		}
		}

		/// <summary>
		/// Call the static function FieldGenerator.Check on button press
		/// </summary>
		[UsedImplicitly]
		public void CheckOnClick () {
			Check();
		}

		private static void SwapPos (int x0, int y0, int x1, int y1) {
			Tile holdTile = _tiles[y0][x0];
			Vector2 start = new Vector2(x0 + 1, y0 + 1);
			Vector2 end = new Vector2(x1 + 1, y1 + 1);

			Animate.Add(new AnimatePosition(holdTile, start, end));

			_tiles[y0][x0] = _tiles[y1][x1];
			_tiles[y1][x1] = holdTile;
		}

		private static void Drop () {
			for (int x = 0; x < _tiles.Count; ++x) {
				for (int y = 0; y < _tiles[0].Count; ++y) {
					Tile t = _tiles[x][y];

					if (t.IsEmpty) {
						continue;
					}

					for (int up = x; up < Height; ++up) {
						int drop = 0;

						for (int down = x; down >= 0; --down) {
							if (_tiles[down][y].IsEmpty) {
								++drop;
							}
						}

						if (drop == 0) {
							continue;
						}

						SwapPos(y, up, y, up - drop);
					}
				}
			}
		}

		[UsedImplicitly]
		private void OnEnable () {
			_tilePrefabs = new List<GameObject>();
			_tiles = new List<List<Tile>>();

			foreach (string element in Enum.GetNames(typeof(Element))) {
				_tilePrefabs.Add(Resources.Load("Tile " + element) as GameObject);
			}
		}

		[UsedImplicitly]
		private void Start () {
			for (int y = 0; y < Height; ++y) {
				List<Tile> row = new List<Tile>();

				for (int x = 0; x < Width; ++x) {
					int e = Random.Range(1, _tilePrefabs.Count);

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
			Animate.Update();
		}

	}

}