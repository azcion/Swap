using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	[UsedImplicitly]
	internal sealed class Z {

		public const int Camera = -10;
		public const int SelectedTileOverlay = 5;
		public const int SelectedTileSprite = SelectedTileOverlay + 1;
		public const int TileSprite = SelectedTileSprite + 1;
		public const int GridBackground = TileSprite + 1;

		public static Vector3 VCamera;
		public static Vector3 VSelectedTileOverlay;
		public static Vector3 VSelectedTileSprite;
		public static Vector3 VTileSprite;
		public static Vector3 VGridBackground;

		static Z () {
			Vector3 f = Vector3.forward;

			VCamera = new Vector3(
				1 + FieldGenerator.Width * FieldGenerator.InitPosHalf - FieldGenerator.InitPosHalf,
				1 + FieldGenerator.Height * FieldGenerator.InitPos - FieldGenerator.InitPos,
				Camera);

			VTileSprite = f * TileSprite;
			VSelectedTileSprite = f * SelectedTileSprite;
			VSelectedTileOverlay = f * SelectedTileOverlay;
			VGridBackground = f * GridBackground;
		}

	}

}