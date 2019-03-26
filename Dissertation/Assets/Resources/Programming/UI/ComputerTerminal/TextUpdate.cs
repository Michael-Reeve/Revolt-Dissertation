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
	public List<Button> buttons;
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
		currentVoltage.text = "Start: " + teslaStart.Voltage.ToString() +" | " + "End: " + teslaEnd.Voltage.ToString();
		GetVoltages();
		otherVoltages.text = connectedVoltages;
		if(teslaEnd.completed == true)
		{
			foreach(Button button in buttons)
				button.interactable = false;
		}
	}

	public void GetVoltages()
	{
		connectedVoltages = "Required Voltage: " + teslaEnd.minVoltage + "<-->" + teslaEnd.maxVoltage + "\n\n";
		electrics = teslaStart.GetConnections(teslaStart);
		foreach(Electric electric in electrics)
		{
			if(electric == teslaStart || electric == teslaEnd)
				continue;
			connectedVoltages +=  "Connection " + (electrics.IndexOf(electric) + 1) + ": " + electric.Voltage + "\n";
		}
	}
}
