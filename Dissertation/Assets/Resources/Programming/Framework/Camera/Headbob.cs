using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour 
{
	public FirstPersonCameraController cameraController;
	public Vector3 velocity;
	public float cameraOffset;
	void LateUpdate()
	{
		velocity = cameraController.possessed.movementVelocity * 100;
		cameraOffset = Mathf.Sin(Time.time * Mathf.Round(velocity.magnitude) * 1.25f) / 25;
		transform.position = new Vector3(transform.position.x, transform.position.y + cameraOffset, transform.position.z);
    	transform.eulerAngles = new Vector3(transform.eulerAngles.x + (cameraOffset * 5), transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
