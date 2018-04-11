using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts {

	public class CameraController : MonoBehaviour {

		[UsedImplicitly]
		private IEnumerator Start () {
			Screen.SetResolution(419, 543, false);

			while (!SplashScreen.isFinished) {
				yield return null;
			}

			Camera.main.transform.position = Z.VCamera;
		}

	}

}