﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettings : MonoBehaviour
{
	public List<Light> lights;
	public List<MeshRenderer> lightObjects;
	public AudioSource soundEffect;
	public bool lightsActive;

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
		lightsActive = false;
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
		if(soundEffect) soundEffect.Play();
		lightsActive = true;
	}

}
