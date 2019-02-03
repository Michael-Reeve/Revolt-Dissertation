using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstPersonCameraController : CameraController, ISave
{
	Vector3 initialRotation, currentRot;
	private UnityAction toggleInput;

	void OnEnable()
	{
		EventManager.StartListening("DisableInput", toggleInput);
	}
	void OnDisable()
	{
		EventManager.StopListening("DisableInput", toggleInput);
	}

	public override void Awake()
	{
		base.Awake();
		toggleInput = new UnityAction (ToggleControls);
		initialRotation = transform.eulerAngles;
		Debug.Log("Camera Rotation Set:" + initialRotation);
		currentRot = initialRotation;
	}

	public override void RotatePossessed(Vector2 input, float sensitivity = 1)
	{		
		float rotX = input.y * sensitivity;
		float rotY = input.x * sensitivity;
		currentRot += new Vector3(-rotY, rotX, 0);
		currentRot.x = Mathf.Clamp(currentRot.x, initialRotation.x - 70, initialRotation.x + 70);
		currentRot.z = 0f;
		transform.eulerAngles = currentRot;
		possessed.transform.eulerAngles = new Vector3(possessed.transform.eulerAngles.x, currentRot.y, possessed.transform.eulerAngles.z);
	}

	void LateUpdate ()
	{
		if (active)
		{
			if (possessed != null)
			{
				MoveToPossessed(smoothed, smoothing);
				RotatePossessed(new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")));
			}	
		}
	}

	public void ToggleControls()
	{
		active = !active;
	}

	public void Save()
	{
		SaveGame.SaveVector3(currentRot, "CameraRot");
	}

	public void Load()
	{
		initialRotation = SaveGame.LoadVector3("CameraRot");
		currentRot = SaveGame.LoadVector3("CameraRot");
		Debug.Log("Camera Rotation Loaded!");
	}
}
