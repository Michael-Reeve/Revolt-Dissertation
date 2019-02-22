using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour 
{
	public Text currentVoltage;
	public Text otherVoltages;
	private List<Electric> electrics;
	private string connectedVoltages;
	public TeslaStart teslaStart;
	public TeslaEnd teslaEnd;
	private UnityAction updateText;

	void Awake()
	{
		updateText = new UnityAction (UpdateText);
	}

	void Start()
	{
		UpdateText();
	}

	
	void OnEnable()
	{
		EventManager.StartListening("UpdateConnections", updateText);
	}

	void OnDisable()
	{
		EventManager.StopListening("UpdateConnections", updateText);
	}

	public void UpdateText()
	{
		currentVoltage.text = "Current Voltage: " +  teslaStart.Voltage;
		GetVoltages();
		otherVoltages.text = connectedVoltages;
	}

	public void GetVoltages()
	{
		connectedVoltages = "Required Voltage: " + teslaEnd.voltageToMeet + "\n\n";
		electrics = teslaStart.GetConnections(teslaStart);
		foreach(Electric electric in electrics)
		{
			if(electric == teslaStart || electric == teslaEnd)
				continue;
			connectedVoltages +=  electrics.IndexOf(electric)+ ": " + electric.Voltage + "\n";
		}
	}
}
