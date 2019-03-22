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
		float realFrequency = frequency * Mathf.Round(velocity.magnitude);
		//Debug.Log("Frequency: " + frequency + " | Real Frequency: " + realFrequency);

		cameraOffset = (Mathf.Sin(Time.time * realFrequency) * amplitude) * Mathf.Round(velocity.magnitude);
		offsetAlpha = Mathf.Round(cameraOffset * (100/ (Mathf.Sin(3.1415f / 2) * amplitude)));

		float test = Mathf.Round(cameraOffset * (100/(Mathf.Sin((3.1415f / 2) * realFrequency) * amplitude)));
		Debug.Log("OffsetAlphaTest: " + test);
		Debug.Log("OffsetAlpha: " + offsetAlpha);
		transform.position = new Vector3(transform.position.x, transform.position.y + cameraOffset, transform.position.z);
    	//transform.eulerAngles = new Vector3(transform.eulerAngles.x + (cameraOffset * 5), transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
