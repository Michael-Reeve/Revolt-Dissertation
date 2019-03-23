using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeslaAnchor : Electric
{
	private UnityAction updateLinks;
	private UnityAction Test;
	
	void OnEnable()
	{
		EventManager.StartListening("UpdateConnections", updateLinks);
	}

	void OnDisable()
	{
		EventManager.StopListening("UpdateConnections", updateLinks);
		voltage = 0;
		Invoke("RefreshLinks", Time.unscaledDeltaTime);
		Difference();
	}

	void RefreshLinks()
	{
		EventManager.TriggerEvent("UpdateConnections");
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
	
}
