using UnityEngine; 

public class KeyboardController : MonoSingleton<KeyboardController> {

	void Start () {
		CheckIsSingleInScene ();
	}

	void Update(){

		// Camera movement.
		CameraController.Instance.AddToCameraMovement (
			new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))
		);
	}
}
