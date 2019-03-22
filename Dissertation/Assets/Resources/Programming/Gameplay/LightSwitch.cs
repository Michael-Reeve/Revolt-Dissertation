using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour 
{
	public LightSwitchDictionary lightSwitches;
	public Dictionary<LightSettings, float> LightSwitches = new Dictionary<LightSettings, float>();
	private bool switching = false;

	void Start()
	{
		for(int i = 0; i < lightSwitches.lightSettings.Count; i++)
		{
			LightSwitches.Add(lightSwitches.lightSettings[i], lightSwitches.timings[i]);
		}
	}

	public void ToggleSwitch()
	{
		if(switching == false)
		{
			switching = true;
			StartCoroutine(TurnOnLights());
		}
	}

	IEnumerator TurnOnLights()
	{
		foreach(LightSettings lightSetting in LightSwitches.Keys)
		{
			float wait;
			LightSwitches.TryGetValue(lightSetting, out wait);
			yield return new WaitForSeconds(wait);
			lightSetting.ActivateLights();
		}
		switching = false;
	}
	
}
[System.Serializable]
public struct LightSwitchDictionary
{
	public List<LightSettings> lightSettings;
	public List<float> timings;
}
