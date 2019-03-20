using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettings : MonoBehaviour 
{
	public List<Light> lights;
	public List<MeshRenderer> lightObjects;

	void Start()
	{
		TurnOffLights();
	}


	public void TurnOffLights()
	{
		foreach(Light light in lights)
		{
			light.gameObject.SetActive(false);
		}
		foreach(MeshRenderer lightObject in lightObjects)
		{
			lightObject.material.DisableKeyword("_EMISSION");
		}
	}

	public void ActivateLights()
	{
		foreach(Light light in lights)
		{
			light.gameObject.SetActive(true);
		}
		foreach(MeshRenderer lightObject in lightObjects)
		{
			lightObject.material.EnableKeyword("_EMISSION");
		}
	}
}
