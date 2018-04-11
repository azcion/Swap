using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Animation {

	internal static class Animate {

		public static int TilesDroppedThisRound;
		public static int TilesDroppedTotal;

		private static readonly List<IAnimate> Animations;

		private static int _animationsPending;

		static Animate () {
			Animations = new List<IAnimate>();
			TilesDroppedTotal = 0;
		}

		/// <summary>
		/// Add an animation to the queue
		/// </summary>
		public static void Add (IAnimate animation) {
			Animations.Add(animation);
			++_animationsPending;
		}

		/// <summary>
		/// Add removal animations of matched tiles
		/// </summary>
		public static void Drop (Tile[,] tiles, Element[,] matched) {
			TilesDroppedThisRound = 0;

			for (int y = 0; y < FieldGenerator.Height; ++y) {
				for (int x = 0; x < FieldGenerator.Width; ++x) {
					if (matched[y, x] == 0) {
						continue;
					}

					Tile t = tiles[y, x];

					if (t.IsEmpty) {
						Debug.LogError("Empty tile was matched.");
						return;
					}

					t.SetEmpty();
					++TilesDroppedThisRound;

					Add(new AnimatePosition(t, Vector2.down, Duration.Medium));
					Add(new AnimateRotation(t));
					Add(new AnimateScale(t));
					Add(new AnimateOpacity(t));
				}
			}

			TilesDroppedTotal += TilesDroppedThisRound;
		}

		public static void Fill (Tile tile) {
			int x = tile.X + 1;
			int y = tile.Y + 1;
			Vector2 start = new Vector2(x, FieldGenerator.Height + FieldGenerator.DropHeight);
			Vector2 end = new Vector2(x, y);

			Add(new AnimatePosition(tile, start, end, Duration.Long));
			Add(new AnimateOpacity(tile, true));
		}

		/// <summary>
		/// Call Update methods of animations and remove them if needed
		/// </summary>
		public static void Update () {
			if (_animationsPending == 0) {
				return;
			}

			List<int> indexesToRemove = new List<int>();

			for (int i = 0; i < Animations.Count; ++i) {
				IAnimate a = Animations[i];

				if (!a.IsActive()) {
					indexesToRemove.Add(i);
					continue;
				}

				a.Update();
			}

			for (int i = 0; i < indexesToRemove.Count; ++i) {
				Animations.RemoveAt(indexesToRemove[i] - i);
			}

			_animationsPending -= indexesToRemove.Count;
		}

	}

}