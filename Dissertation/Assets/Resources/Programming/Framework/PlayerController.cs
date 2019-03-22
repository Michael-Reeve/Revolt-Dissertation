using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Controller
{
	private UnityAction KeyPressListener;
	private UnityAction KeyDownListener;
	private UnityAction impact;
	private UnityAction toggleInput;
	public Attributes attributes;
	public List<Interactible> targettedInteractible = new List<Interactible>();
	public MainMenu mainMenu;
	public Canvas gui;
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

	public RaycastHit Raycast()
	{
		RaycastHit raycastHit;
		Physics.Raycast(activeCamera.transform.position, activeCamera.transform.forward, out raycastHit, attributes.interactRange);
		return raycastHit;
	}

	void LateUpdate()
	{
		RaycastHit raycastHit = Raycast();;
		if(raycastHit.collider != null)
		{
			List<Interactible> interactible = Utility.GetInterface<Interactible>(raycastHit.collider.gameObject.GetComponents<MonoBehaviour>());
			if(interactible.Count > 0 && interactible[0] != null)
			{
				targettedInteractible = interactible;
				targettedInteractible.TrimExcess();
			}
			else
			{
				targettedInteractible.Clear();
			}
		}
		else
		{
			targettedInteractible.Clear();
		}
		if(Input.GetAxis("Mouse ScrollWheel") != 0 && active)
		{
			if(inventory.equippedItem == null)
				inventory.GUI.HighlightItem(Input.GetAxis("Mouse ScrollWheel"));
		}
		transform.position = possessed.transform.position;
		possessed.speedModifier = Input_Manager.shiftModifier ? 2: 1;
	}

	void KeyPress()
	{
		if(active)
		{
			if(Input.GetKeyDown("mouse 0") == true && possessed.IsGrounded() == true)
			{
				if(targettedInteractible.Count > 0)
				{
					//Debug.Log(targettedInteractible.Count);
					foreach(Interactible interactible in targettedInteractible)
					{
						Debug.Log(interactible);
						if(interactible != null)
							interactible.Interact(this);
					}
				}
				Debug.DrawRay(activeCamera.transform.position, activeCamera.transform.forward, Color.red, 5f);
			}
			if(Input.GetKeyDown("e") == true)
			{
				if(Input_Manager.shiftModifier)
					inventory.RemoveItem(inventory.items[inventory.GUI.highlightedItem]);
				inventory.UseItem(inventory.GUI.highlightedItem);
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

	public void SaveData(string data)
	{
		data = string.Concat (data, " " + Time.time);
		JSON.SaveUsage(GameManager.saveProfile, data);
	}

	public void ToggleMainMenu()
	{
		mainMenu.ToggleActive();
		SetUIVisible(!mainMenu.gameObject.activeInHierarchy);
	}

	public void SetUIVisible(bool value)
	{
		if(gui)
			gui.gameObject.SetActive(value);
	}
}
