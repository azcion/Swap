﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Animation {

	internal static class Animate {

		public static int TilesPoppedThisRound { get; private set; }
		public static int TilesPoppedTotal { get; private set; }
		
		private static readonly List<IAnimate> Animations;

		private static int _animationsPending;

		static Animate () {
			Animations = new List<IAnimate>();
			TilesPoppedThisRound = 0;
			TilesPoppedTotal = 0;
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
		public static void Pop (Tile[,] tiles, Element[,] matched) {
			TilesPoppedThisRound = 0;
			float delay = 0;

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
					++TilesPoppedThisRound;

					FieldGenerator.Instance.StartCoroutine(DelayedPop(delay, t));
					delay += Duration.SinglePopDelay;
				}
			}

			TilesPoppedTotal += TilesPoppedThisRound;
		}

		/// <summary>
		/// Add fill animation for tile
		/// </summary>
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

		private static IEnumerator DelayedPop (float seconds, Tile tile) {
			yield return new WaitForSeconds(seconds);

			Add(new AnimatePosition(tile, Vector2.down, Duration.Long));
			Add(new AnimateRotation(tile));
			Add(new AnimateScale(tile));
			Add(new AnimateOpacity(tile));
		}

	}

}