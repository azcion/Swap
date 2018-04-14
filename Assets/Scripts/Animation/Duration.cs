﻿namespace Assets.Scripts.Animation {

	public static class Duration {

		public const float Wait = SinglePopDelay * FieldGenerator.Width * FieldGenerator.Height;
		public const float SafeWait = Wait + Long;

		#region Animation duration
		public const float Short = .2f;
		public const float Medium = .4f;
		public const float Long = .6f;
		#endregion

		public const float SinglePopDelay = .03f;
		public const float SingleFillDelay = .02f;

		public const float DragTime = 5;

	}

}