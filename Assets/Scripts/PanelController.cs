using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

internal sealed class PanelController : MonoBehaviour {

	public const int MaxPanels = 5;

	public static int[] RoundValuesPerElement;

	private static readonly int ElementCount;
	private static readonly Panel[] Panels;
	private static readonly Sprite[] PanelSprites;

	static PanelController () {
		ElementCount = Enum.GetValues(typeof(Element)).Length;
		Panels = new Panel[MaxPanels];
		PanelSprites = new Sprite[ElementCount];
	}

	public static Text GetTextObject (int index) {
		return Panels[index].Text;
	}
		
	/// <summary>
	/// Add values earned during this round to corresponding panels
	/// </summary>
	public static void AssignRoundValues () {
		foreach (Panel panel in Panels) {
			panel.AddValue(RoundValuesPerElement[(int) panel.Type]);
			panel.Update();
		}

		RoundValuesPerElement = new int[ElementCount];
	}

	/// <summary>
	/// Show correct backgrounds for the panels' corresponding types
	/// </summary>
	private static void AssignBackgrounds () {
		foreach (Panel panel in Panels) {
			panel.Renderer.sprite = PanelSprites[(int) panel.Type];
		}
	}

	private void Debug () {
		for (int i = 0; i < 5; ++i) {
			Panels[i] = new Panel(i, transform.GetChild(i), (Element) (i + 2), 10);
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