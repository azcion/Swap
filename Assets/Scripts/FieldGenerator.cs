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
		private static List<Animation> _animations;

		private List<GameObject> _tilePrefabs;

		/// <summary>
		/// Move overlapped tile position to empty spot
		/// </summary>
		public static void Swap (Vector2 tile, Vector2 empty) {
			int x0 = (int) tile.x - 1;
			int y0 = (int) tile.y - 1;
			int x1 = (int) empty.x - 1;
			int y1 = (int) empty.y - 1;

			Tile holdTile = _tiles[y0][x0];

			Animation a = new Animation(holdTile.GO.transform, tile, empty);
			a.Start();
			_animations.Add(a);

			_tiles[y0][x0] = _tiles[y1][x1];
			_tiles[y1][x1] = holdTile;
		}

		public void Check () {
			Match.Check(_tiles);
		}

		[UsedImplicitly]
		private void OnEnable () {
			_tilePrefabs = new List<GameObject>();
			_tiles = new List<List<Tile>>();
			_animations = new List<Animation>();

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
			for (int i = 0; i < _animations.Count; ++i) {
				Animation a = _animations[i];

				if (!a.Active) {
					_animations.RemoveAt(i);
					return;
				}

				a.Update();
			}
		}

	}

}