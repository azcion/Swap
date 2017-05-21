using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts {

	public class CameraController : MonoBehaviour {

		[UsedImplicitly]
		private void Start () {
			Camera.main.transform.position = Z.VCamera;
		}

	}

}