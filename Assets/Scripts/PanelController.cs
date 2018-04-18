using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	internal sealed class PanelController : MonoBehaviour {

		public static int[] RoundValuesPerElement;

		private const int MaxPanels = 5;

		private static readonly int ElementCount;
		private static readonly Panel[] Panels;
		private static readonly Sprite[] PanelSprites;
		private static readonly SpriteRenderer[] PanelRenderers;

		static PanelController () {
			ElementCount = Enum.GetValues(typeof(Element)).Length;
			Panels = new Panel[MaxPanels];
			PanelRenderers = new SpriteRenderer[MaxPanels];
			PanelSprites = new Sprite[ElementCount];
		}

		/// <summary>
		/// Add values earned during this round to corresponding panels
		/// </summary>
		public static void AssignRoundValues () {
			foreach (Panel panel in Panels) {
				panel.AddValue(RoundValuesPerElement[(int) panel.Type]);
			}

			RoundValuesPerElement = new int[ElementCount];
		}

		/// <summary>
		/// Show correct backgrounds for the panels' corresponding types
		/// </summary>
		private static void AssignBackgrounds () {
			for (int i = 0; i < Panels.Length; ++i) {
				PanelRenderers[i].sprite = PanelSprites[(int)Panels[i].Type];
			}
		}

		private static void Debug () {
			for (int i = 0; i < 5; ++i) {
				Panels[i] = new Panel((Element) (i + 2), 10);
			}

			AssignBackgrounds();
		}

		public static void DebugValues () {
			string log = "";

			foreach (int value in RoundValuesPerElement) {
				log += value + " ";
			}

			UnityEngine.Debug.Log(log);
		}

		[UsedImplicitly]
		private void OnEnable () {
			Vector3 v = transform.localPosition;
			transform.localPosition = new Vector3(v.x, v.y, Z.Panel);
			RoundValuesPerElement = new int[ElementCount];

			for (int i = 0; i < MaxPanels; ++i) {
				PanelRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
			}

			string[] elementNames = Enum.GetNames(typeof(Element));

			for (int i = 0; i < elementNames.Length; i++) {
				PanelSprites[i] = Resources.Load<Sprite>("Sprites/Panel " + elementNames[i]);
			}
		}

		[UsedImplicitly]
		private void Start () {
			Debug();
		}

	}

}