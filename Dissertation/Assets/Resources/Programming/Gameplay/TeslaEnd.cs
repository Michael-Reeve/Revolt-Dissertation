using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeslaEnd : Electric
{
	public bool completed;
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
		if(Voltage > voltageToMeet && !completed)
		{
			completed = true;
			foreach(Electric anchor in GetConnectionsFrom(this))
			{
				Debug.Log(anchor.name);
				if(anchor.GetComponent<PickUp>())
					anchor.GetComponent<PickUp>().active = false;
				if(anchor.GetComponent<LightningEffect>())
				{
					Debug.Log("AssertYes");
					anchor.GetComponent<LightningEffect>().RemoveEffects();
				}
			}
			Debug.Log("Circuit Complete!");
			if(completitionEvent != null)
				completitionEvent.Invoke();
		}
	}

}
