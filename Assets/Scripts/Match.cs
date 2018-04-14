namespace Assets.Scripts {

	internal static class Match {

		private static Element[,] _toDestroy;

		/// <summary>
		/// Check the field for matches of 3 or more and mark them for destruction
		/// </summary>
		public static Element[,] Check (Tile[,] field) {
			_toDestroy = new Element[FieldGenerator.Height, FieldGenerator.Width];

			FindHorizontal(field);
			FindVertical(field);

			return _toDestroy;
		}

		private static void FindHorizontal (Tile[,] field) {
			for (int y = 0; y < FieldGenerator.Height; ++y) {
				Element type = field[y, 0].Type;
				int matches = 0;

				for (int x = 0; x < FieldGenerator.Width; ++x) {
					Element e = field[y, x].Type;

					if (e == type) {
						++matches;

						if (matches > 2 && x == FieldGenerator.Width - 1) {
							for (int i = 0; i < matches; ++i) {
								_toDestroy[y, x - i] = type;
							}
						}
					} else {
						if (matches > 2) {
							for (int i = 0; i < matches; ++i) {
								_toDestroy[y, x - 1 - i] = type;
							}
						}

						type = e;
						matches = 1;
					}
				}
			}
		}

		private static void FindVertical (Tile[,] field) {
			for (int x = 0; x < FieldGenerator.Width; ++x) {
				Element type = field[0, x].Type;
				int matches = 0;

				for (int y = 0; y < FieldGenerator.Height; ++y) {
					Element e = field[y, x].Type;

					if (e == type) {
						++matches;

						if (matches > 2 && y == FieldGenerator.Height - 1) {
							for (int i = 0; i < matches; ++i) {
								_toDestroy[y - i, x] = type;
							}
						}
					} else {
						if (matches > 2) {
							for (int i = 0; i < matches; ++i) {
								_toDestroy[y - 1 - i, x] = type;
							}
						}

						type = e;
						matches = 1;
					}
				}
			}
		}

	}

}