using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour 
{
	public FirstPersonCameraController cameraController;
	public Vector3 velocity;
	[Space]
	[Header("Settings")]
	public float amplitude = 1f;
	public float frequency = 1f;
	public float intensity = 1.5f;
	[Space]
	public float offsetAlpha;
	public float cameraOffset;


	void Update()
	{
		velocity = cameraController.possessed.movementVelocity * 5;
		Debug.Log(velocity.magnitude);
		float realFrequency = frequency * Mathf.Round(velocity.magnitude);
		//Debug.Log(Real Frequency: " + realFrequency);

		cameraOffset = (Mathf.Sin(Time.time * realFrequency) * amplitude) * Mathf.Round(velocity.magnitude);
		offsetAlpha = Mathf.Round(cameraOffset * (100/ (Mathf.Sin(3.1415f / 2) * amplitude)));
		//Debug.Log("OffsetAlpha: " + offsetAlpha);
	}

	public void SetCameraPosition()
	{
		transform.position = Vector3.Lerp(cameraController.possessed.transform.position + cameraController.offset, new Vector3(transform.position.x, transform.position.y + cameraOffset, transform.position.z), 1f * Time.deltaTime);
	}
	public void SetCameraRotation()
	{
    	//transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x + (cameraOffset * intensity), transform.eulerAngles.y, transform.eulerAngles.z), Time.time * Time.deltaTime);
	}
}
