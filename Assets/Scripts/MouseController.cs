using UnityEngine;
using System;

/// <summary>
/// Mouse controller. Handle mouse clicking and zooming.
/// </summary>
public class MouseController : MonoSingleton<MouseController> {

	void Start(){
		CheckIsSingleInScene ();
	}

	void Update(){
		// Camera Zoom.
		// FIXME: Deal with twitchy scroll.
		CameraController.Instance.AddToCameraMovement (
			new Vector3 (0, -Input.GetAxis ("Mouse ScrollWheel"), 0)
		);

	}


}
