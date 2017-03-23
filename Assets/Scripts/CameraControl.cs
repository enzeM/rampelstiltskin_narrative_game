using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	//target that camera need to follow
	[SerializeField]
	private GameObject followTarget;
	[SerializeField]
	private float cameraSpeed;

	// Update is called once per frame
	void Update () {
		HandleCameraMovement ();	
	}

	//camera follow a specific gameobject with a preset speed
	void HandleCameraMovement () {
		Vector3 fromPos = this.transform.position;
		Vector3 toPos = new Vector3 (followTarget.transform.position.x
			, followTarget.transform.position.y
			, fromPos.z);
		this.transform.position = Vector3.Lerp (fromPos, toPos, cameraSpeed);
	}
}
