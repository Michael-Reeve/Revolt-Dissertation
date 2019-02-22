﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeslaEnd : Electric
{
	public UnityEvent completitionEvent;
	private UnityAction updateLinks;
	public int voltageToMeet = 30;

	void Awake()
	{
		chargeAction = CheckVoltage;
		//updateLinks = new UnityAction (completitionEvent);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0, 0.4f);
		Gizmos.DrawSphere(transform.position, (maxRadius / 100 * Voltage));
	}

	public void CheckVoltage()
	{
		if(Voltage > voltageToMeet)
		{
			Debug.Log("Circuit Complete!");
			if(completitionEvent != null)
				completitionEvent.Invoke();
		}
	}

}
