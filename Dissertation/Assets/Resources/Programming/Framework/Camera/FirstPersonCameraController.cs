using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstPersonCameraController : CameraController, ISave
{
	Vector3 initialRotation, currentRot;
	private UnityAction toggleInput;
	private Headbob headbob;

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
		currentRot = initialRotation;
		if(GetComponent<Headbob>())
			headbob = GetComponent<Headbob>();
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

	void Update ()
	{
		if (active)
		{
			if (possessed != null)
			{
				MoveToPossessed(smoothed, smoothing);
				RotatePossessed(new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")));
				if(headbob)
					{ headbob.SetCameraPosition(); headbob.SetCameraRotation(); }
			}	
		}
	}

	public void ToggleControls()
	{
		active = !active;
	}

	public void LoadData(ObjectData objectData)
	{
		if(objectData.position == Vector3.zero)
			return;
		transform.position = objectData.position;
		transform.rotation = objectData.rotation;
		initialRotation = objectData.rotation.eulerAngles;
		currentRot = objectData.rotation.eulerAngles;
		transform.parent = objectData.parent;
	}

	public void Save()
	{
		GameManager.instance.AddLevelData(this.gameObject.name, new ObjectData(this.gameObject.activeInHierarchy, possessed.transform.position, transform.rotation, transform.parent));
	}

	public void Load()
	{
		if(GameManager.instance.levelDictionary != null)
		{
			ObjectData loadData = new ObjectData();
			Debug.Log(gameObject.name + " | " + GameManager.instance.levelDictionary.ContainsKey(gameObject.name));
			GameManager.instance.levelDictionary.TryGetValue(this.gameObject.name, out loadData);
			LoadData(loadData);
			Debug.Log("Loading Data for " + this.name);
		}
	}
}
