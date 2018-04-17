using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts {

	internal sealed class CameraController : MonoBehaviour {

		[UsedImplicitly]
		private IEnumerator Start () {
			Screen.SetResolution(504, 896, false);

			while (!SplashScreen.isFinished) {
				yield return null;
			}

			Vector3 v = Camera.main.transform.position;
			Camera.main.transform.position = new Vector3(v.x, v.y, Z.Camera);
		}

	}

}