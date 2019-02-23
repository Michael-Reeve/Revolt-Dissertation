using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour 
{
	public FirstPersonCameraController cameraController;
	public Vector3 velocity;
	public float notsure;
	void LateUpdate()
	{
		velocity = cameraController.possessed.movementVelocity * 100;
		notsure = Mathf.Sin(Time.time * Mathf.Round(velocity.magnitude) * 1.5f) / 25;
		transform.position = new Vector3(transform.position.x, transform.position.y + notsure, transform.position.z);
    	transform.eulerAngles = new Vector3(transform.eulerAngles.x + (notsure * 5), transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
