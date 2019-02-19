using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeslaStart : Electric
{
	private UnityAction updateLinks;
	
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

	private void UpdateLinksEvent()
	{
		ClearConnections();
		GetConductors();
		foreach(Electric electric in conductingTo)
		{
			Debug.Log(electric.name);
		}
		Debug.Log(conductingTo.Count + " Radius: " + (arcRadius/100 * Voltage));
		CreateArc(conductingTo);
		ChargeConductors();
	}

	public void AddVoltage(int voltage)
	{
		Voltage += voltage;
		this.chargeAction();
	}
	
	public void Interact(PlayerController controller)
	{
		if (controller.inventory.equippedItem != null)
		{
			Voltage += 5;
			this.chargeAction();
		}
	}
}
