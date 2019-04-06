using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, ISave
{
	public LightSwitchDictionary lightSwitches;
	public Dictionary<LightSettings, float> LightSwitches = new Dictionary<LightSettings, float>();
	private bool switching = false;
	private string uniqueID;
	private bool lightsActive;

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
		lightsActive = true;
	}

	public void LoadData(ObjectData objectData)
	{
		if(objectData.position == Vector3.zero)
			return;
		lightsActive = objectData.active;
		if(lightsActive)
		{
			foreach(LightSettings lightSetting in LightSwitches.Keys)
			{
				lightSetting.ActivateLights();
			}
		}
	}

	public void Save()
	{
		GameManager.instance.AddLevelData(uniqueID, new ObjectData(lightsActive, transform.position, transform.rotation, transform.parent));
	}

	public void Load()
	{
		if(uniqueID == null)
			uniqueID = GetUniqueID();
		if(GameManager.instance.levelDictionary != null)
		{
			ObjectData loadData = new ObjectData();
			Debug.Log(gameObject.name + " | " + GameManager.instance.levelDictionary.ContainsKey(uniqueID));
			GameManager.instance.levelDictionary.TryGetValue(uniqueID, out loadData);
			LoadData(loadData);
			Debug.Log("Loading Data for " + this.name);
		}
	}

	public string GetUniqueID()
	{
		return string.Format(this.gameObject.name + "{0}" + "{1}" + "{2}", this.transform.position, this.transform.rotation, this.transform.parent);
	}
	
}
[System.Serializable]
public struct LightSwitchDictionary
{
	public List<LightSettings> lightSettings;
	public List<float> timings;
}
