using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeslaAnchor : Electric, Interactible
{
	public Item pickUp;
	private UnityAction updateLinks;
	private UnityAction Test;
	
	void OnEnable()
	{
		EventManager.StartListening("UpdateConnections", updateLinks);
	}

	void OnDisable()
	{
		EventManager.StopListening("UpdateConnections", updateLinks);
	}

	void Awake()
	{
		updateLinks = new UnityAction (UpdateLinksEvent);
		chargeAction = UpdateLinks;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0, 0.4f);
		Gizmos.DrawSphere(transform.position, (arcRadius/100 * Voltage));
	}

	public void Interact(PlayerController controller)
	{
		ClearConnections();
		controller.GetComponent<Inventory>().AddItem(pickUp);
		//EventManager.TriggerEvent("UpdateConnections");
		this.gameObject.SetActive(false);
	}

	public void UpdateLinks(Electric origin = null)
	{
		if(origin != null)
		{
			if(conductingTo.Contains(origin))
			{
				Debug.Log("Caught recursion!");
				return;
			}
		}
		GetConductors();
		CreateArc(conductingTo);
		ChargeConductors();
	}

	public void UpdateLinksEvent()
	{
		GetConductors();
		foreach(Electric electric in conductingTo)
		{
			Debug.Log(electric.name);
		}
		Debug.Log(conductingTo.Count + " Radius: " + (arcRadius/100 * Voltage));
		CreateArc(conductingTo);
		ChargeConductors();
	}

	public void TestEvent()
	{
		Debug.Log("Test");
	}
	
}
