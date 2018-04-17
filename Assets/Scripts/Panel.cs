namespace Assets.Scripts {

	public class Panel {

		public readonly Element Type;
		public readonly int Health;

		public int Value { get; private set; }

		public Panel (Element type, int health) {
			Type = type;
			Health = health;
		}

		public void AddValue (int value) {
			Value += value;
		}

		public void ResetValue () {
			Value = 0;
		}

	}

}