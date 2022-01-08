using UnityEngine;
using UnityEngine.UI;

public class ObjectHooks : MonoBehaviour {

	public static Text[] PanelTextComponents;
	
	public static Slider TimerBar;
	public static GameObject TimerBarBackground;
	public static GameObject TimerBarFill;
	public static Image TimerBarImage;

	[SerializeField] private RectTransform panelValues;
	[SerializeField] private Slider timerBar;

	private void Awake () {
		PanelTextComponents = panelValues.GetComponentsInChildren<Text>();

		TimerBar = timerBar;
		TimerBarBackground = timerBar.transform.GetChild(0).gameObject;
		TimerBarFill = timerBar.transform.GetChild(1).gameObject;
		TimerBarImage = TimerBarFill.transform.GetChild(0).GetComponent<Image>();
	}

}