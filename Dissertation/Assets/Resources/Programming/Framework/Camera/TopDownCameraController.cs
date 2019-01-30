using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TopDownCameraController : CameraController
{
	public float distance;
	protected bool constrained;

	public override void Impact()
	{
		//active = !active;
	}

	private Vector3 possessedLocation()
	{
		return new Vector3(possessed.transform.position.x, distance, possessed.transform.position.z);
	}

	public override void MoveToPossessed(bool smoothed, float smoothing = 0)
	{
		if(smoothed == false)
			transform.position = possessedLocation();
		else
		{
			if(constrained == true)
			{
				Vector3 difference = possessedLocation() - transform.position;
				//transform.position = transform.position + (possessedLocation() - transform.position);
				transform.position = Vector3.SmoothDamp(transform.position, transform.position + difference, ref velocity, Time.deltaTime * (smoothing * 5));
			}
			else
				transform.position = Vector3.SmoothDamp(transform.position, possessedLocation(), ref velocity, Time.deltaTime * (smoothing * 10));
		}
	}

	public override void RotatePossessed(Vector2 input, float sensitivity = 1f)
	{
		input = new Vector2(input.x * -1 * sensitivity, input.y * sensitivity);
		transform.eulerAngles += new Vector3(input.x, input.y, 0);
		transform.eulerAngles = ClampPossessed(clampXY.x, clampXY.y);
		possessed.transform.eulerAngles = transform.eulerAngles;
	}

	public override bool possessedVisible()
	{
		Vector3 screenPoint = thisCamera.WorldToViewportPoint(possessedLocation());
		Debug.Log(screenPoint);
		bool onScreen = screenPoint.x > 0.1 && screenPoint.y > 0.1 && screenPoint.x < 0.9 && screenPoint.y < 0.9;
		return onScreen;
	}

	Vector3 ClampPossessed(float xClamp = 0, float yClamp = 0)
	{
		Vector3 clampedRot = transform.eulerAngles;		
		if (xClamp != 0)
			clampedRot.x = Mathf.Clamp(clampedRot.x, xClamp * -1, xClamp);
		if (yClamp != 0)
			clampedRot.y = Mathf.Clamp(clampedRot.y, yClamp * -1, yClamp);
		return clampedRot;
	}

	void ConstrainCamera()
	{
		if(IsOnScreen(possessed.transform.position))
		{
			constrained = false;
		}
		else
		{
			constrained = true;
		}
	}


	
	void LateUpdate ()
	{
		//Debug.Log(smoothing + " V " + smoothingAmount);
		if (active)
		{
			if (possessed != null)
			{
				ConstrainCamera();
				MoveToPossessed(smoothed, smoothing);
				//RotatePossessed(new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")));
			}
				
		}
	}



 
}
