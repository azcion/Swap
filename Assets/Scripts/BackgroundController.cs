using JetBrains.Annotations;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	[UsedImplicitly]
	private void OnEnable () {
		transform.localPosition = Z.VBackground;
	}

}