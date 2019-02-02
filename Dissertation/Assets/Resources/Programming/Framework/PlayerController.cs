using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Controller, ISave
{
	private UnityAction KeyPressListener;
	private UnityAction KeyDownListener;
	private UnityAction impact;
	private UnityAction toggleInput;
	public Attributes attributes;
	public Interactible targettedInteractible;
	public MainMenu mainMenu;
	private Vector3 position;

	void OnEnable()
	{
		EventManager.StartListening("PlayerCollision", impact);
		EventManager.StartListening("InputKeyPress", KeyPressListener);
		EventManager.StartListening("InputKey", KeyDownListener);
		EventManager.StartListening("DisableInput", toggleInput);
	}

	void OnDisable()
	{
		EventManager.StopListening("PlayerCollision", impact);
		EventManager.StopListening("InputKeyPress", KeyPressListener);
		EventManager.StopListening("InputKey", KeyDownListener);
		EventManager.StopListening("DisableInput", toggleInput);
	}

	void Awake()
	{
		possessed.gameObject.layer = layer;
		KeyPressListener = new UnityAction (KeyPress);
		KeyDownListener = new UnityAction (KeyDown);
		impact = new UnityAction (Impact);
		toggleInput = new UnityAction (ToggleControls);
		Active(true);
		Input_Manager.LockCursor(true);
		if(inventory == true)
			inventory.controller = this;
	}

	void Active(bool value)
	{
		active = value;
	}

	void Impact()
	{
		Debug.Log("Ouch");
	}

	void LateUpdate()
	{
		RaycastHit raycastHit;
		if(Physics.Raycast(activeCamera.transform.position, activeCamera.transform.forward, out raycastHit, attributes.interactRange))
		{
			Interactible interactible = Utility.GetInterface<Interactible>(raycastHit.collider.gameObject.GetComponent<MonoBehaviour>());
			if(interactible != null)
			{
				targettedInteractible = interactible;
			}
		}
		else
		{
			targettedInteractible = null;
		}
		if(Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			inventory.GUI.HighlightItem(Input.GetAxis("Mouse ScrollWheel"));
		}
		transform.position = possessed.transform.position;
	}

	void KeyPress()
	{
		if(active)
		{
			if(Input.GetKeyDown("mouse 0") == true && possessed.IsGrounded() == true)
			{
				if(targettedInteractible != null)
				{
					targettedInteractible.Interact(this);
				}
				Debug.DrawRay(activeCamera.transform.position, activeCamera.transform.forward, Color.red, 5f);
			}
			if(Input.GetKeyDown("e") == true)
			{
				inventory.RemoveItem(inventory.items[inventory.GUI.highlightedItem]);
			}
		}
		if(Input.GetKeyDown("escape") == true)
		{
			ToggleMainMenu();
		}
	}

	void KeyDown()
	{
		
	}

	void ToggleControls()
	{
		Active(!active);
	}

	public void ToggleMainMenu()
	{
		mainMenu.ToggleActive();
	}

	public void Save()
	{
		Debug.Log(gameObject.name + " Saved game!");
		//Debug.Log("Body Rotation On End: " + possessed.rigidBody.transform.eulerAngles);
		SaveGame.SavePlayerPosition(possessed.transform.position);
		SaveGame.SavePlayerRotation(possessed.transform.rotation);
		PlayerPrefs.Save();
	}

	public void Load()
	{
		Debug.Log(gameObject.name + " Loaded game!");
		possessed.transform.position = SaveGame.LoadPlayerPosition();
		transform.position = SaveGame.LoadPlayerPosition();
		possessed.transform.rotation = SaveGame.LoadPlayerRotation();
		activeCamera.transform.rotation = SaveGame.LoadPlayerRotation();
		//Debug.Log("Saved Rotation: " + SaveGame.LoadPlayerRotation());
		//Debug.Log("Body Rotation: " + possessed.rigidBody.transform.eulerAngles);
	}
}
