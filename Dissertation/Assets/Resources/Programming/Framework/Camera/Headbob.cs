using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour 
{
	public FirstPersonCameraController cameraController;
	public Vector3 velocity;
	public float amplitude = 1f;
	public float frequency = 1f;
	public float offsetAlpha;
	public float cameraOffset;


	void FixedUpdate()
	{
		velocity = cameraController.possessed.movementVelocity * 10;

		cameraOffset = (Mathf.Sin(Time.time * frequency) * amplitude) * Mathf.Round(velocity.magnitude);
		offsetAlpha = Mathf.Round(cameraOffset * (100/(Mathf.Sin(3.1415f / 2) * amplitude)));

		//Debug.Log("OffsetAlpha: " + offsetAlpha);
		transform.position = new Vector3(transform.position.x, transform.position.y + cameraOffset, transform.position.z);
    	//transform.eulerAngles = new Vector3(transform.eulerAngles.x + (cameraOffset * 5), transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
