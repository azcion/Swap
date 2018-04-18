using System;
using System.Collections;
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
		public const int DropHeight = 1;

		public static FieldGenerator Instance;

		private static Tile[,] _tiles;
		private static List<GameObject> _tilePrefabs;

		/// <summary>
		/// Start a coroutine that finds, removes and replaces matched tiles
		/// </summary>
		public static void Check () {
			Drag.Lock = true;
			Drag.AllowUnlock = false;
			Instance.StartCoroutine(UpdateField());
		}

		/// <summary>
		/// Move overlapped tile position to empty spot
		/// </summary>
		public static void Swap (Vector2 start, Vector2 end) {
			SwapPos((int)start.x - 1, (int)start.y - 1, (int)end.x - 1, (int)end.y - 1);
		}

		/// <summary>
		/// Find, remove and replace matched tiles, then restart if a match was found
		/// </summary>
		private static IEnumerator UpdateField () {
			yield return new WaitForSeconds(Duration.Short);
			Animate.Pop(_tiles, Match.Check(_tiles));

			if (Animate.TilesPoppedThisRound == 0) {
				Drag.AllowUnlock = true;
				yield break;
			}
			
			yield return new WaitForSeconds(Duration.Medium);
			Drop();

			yield return new WaitForSeconds(Duration.Wait);
			Fill();
			PanelController.AssignRoundValues();
			Drag.Lock = true;
			
			yield return new WaitForSeconds(Duration.SafeWait);
			Check();
		}

		/// <summary>
		/// Swap the position of two tiles at x0, y0 and x1, y1 indices
		/// </summary>
		private static void SwapPos (int x0, int y0, int x1, int y1) {
			Tile holdTile = _tiles[y0, x0];
			Vector2 start = new Vector2(x0 + 1, y0 + 1);
			Vector2 end = new Vector2(x1 + 1, y1 + 1);

			Animate.Add(new AnimatePosition(holdTile, start, end, Duration.Short));

			_tiles[y0, x0] = _tiles[y1, x1];
			_tiles[y1, x1] = holdTile;
		}

		/// <summary>
		/// Make tiles drop to the bottom of their columns
		/// </summary>
		private static void Drop () {
			for (int y = 0; y < Height; ++y) {
				for (int x = 0; x < Width; ++x) {
					Tile t = _tiles[y, x];

					if (t.IsEmpty) {
						continue;
					}

					for (int up = y; up < Height; ++up) {
						int drop = 0;

						for (int down = y; down >= 0; --down) {
							if (_tiles[down, x].IsEmpty) {
								++drop;
							}
						}

						if (drop == 0) {
							continue;
						}

						SwapPos(x, up, x, up - drop);
					}
				}
			}
		}

		/// <summary>
		/// Create new random tiles on all empty fields
		/// </summary>
		private static void Fill () {
			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					Tile oldTile = _tiles[y, x];

					if (oldTile.IsEmpty == false) {
						continue;
					}

					CreateTile(y, x, Random.Range(1, _tilePrefabs.Count), oldTile.Transform.parent, true);
					oldTile.Destroy();
				}
			}
		}

		/// <summary>
		/// Create a new tile of the specified element at position y, x
		/// </summary>
		private static void CreateTile (int y, int x, int element, Transform parent, bool fill = false) {
			GameObject go = Instantiate(_tilePrefabs[element], parent);
			go.name = "T " + x + " " + y;

			Transform t = go.transform;

			t.position = new Vector2(
				InitPos + x,
				InitPos + y + (fill ? DropHeight : 0));

			t.GetChild(0).localPosition = Z.VSelectedTileOverlay;
			t.GetChild(1).localPosition = Z.VTileSprite;

			Tile tile = go.AddComponent<Tile>();
			tile.Initialize(go, y, x, (Element) element);
			_tiles[y, x] = tile;

			if (fill) {
				// Set the alpha to 0 to prevent spawn flicker
				tile.SpriteTransform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
				Animate.Fill(tile);
			}
		}

		[UsedImplicitly]
		private void OnEnable () {
			Instance = this;
			_tilePrefabs = new List<GameObject>();
			_tiles = new Tile[Height, Width];

			foreach (string element in Enum.GetNames(typeof(Element))) {
				_tilePrefabs.Add(Resources.Load("Tile " + element) as GameObject);
			}
		}

		[UsedImplicitly]
		private void Start () {
			for (int y = 0; y < Height; ++y) {
				for (int x = 0; x < Width; ++x) {
					CreateTile(y, x, Random.Range(1, _tilePrefabs.Count), transform);
				}
			}
		}

		[UsedImplicitly]
		private void Update () {
			Animate.Update();
		}

	}

}