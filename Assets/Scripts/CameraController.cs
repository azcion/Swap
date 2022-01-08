using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using Debug = System.Diagnostics.Debug;

internal sealed class CameraController : MonoBehaviour {

	public static Camera Main;
	
	[UsedImplicitly]
	private IEnumerator Start () {
		Main = Camera.main;
		Debug.Assert(Main != null, nameof(Main) + " != null");
		
		while (!SplashScreen.isFinished) {
			yield return null;
		}

		Transform camTransform = Main.transform;
		Vector3 v = camTransform.position;
		camTransform.position = new Vector3(v.x, v.y, Z.Camera);
	}

}