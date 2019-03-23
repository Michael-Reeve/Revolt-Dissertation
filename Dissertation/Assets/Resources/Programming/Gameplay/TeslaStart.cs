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
		updateLinks = new UnityAction (UpdateLinks);
		chargeAction = UpdateLinks;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0, 0.4f);
		Gizmos.DrawSphere(transform.position, GetRadius());
	}

	public void UpdateLinks()
	{
		UpdateConnections();
		onVoltageChange.Invoke();
	}

	public void AddVoltage(int voltage)
	{
		Voltage += voltage;
		this.chargeAction();
	}
}
