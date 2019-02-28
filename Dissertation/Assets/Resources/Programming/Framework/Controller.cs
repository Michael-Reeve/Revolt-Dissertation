﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller : MonoBehaviour, ISave
{
	public bool active;
	public Character possessed;
	protected int layer = 9;
	public Inventory inventory;
	public Camera activeCamera;
	public void Possess(Character newCharacter)
	{
		if(possessed != null)
		{
			possessed.gameObject.layer = 0;
		}
		possessed = newCharacter;
		possessed.gameObject.layer = layer;
	}

	public void LoadData(ObjectData objectData)
	{
		if(objectData.position == Vector3.zero)
			return;
		possessed.transform.position = objectData.position;
		possessed.transform.rotation = objectData.rotation;
		activeCamera.transform.rotation = objectData.rotation;
		possessed.transform.parent = objectData.parent;
	}

	public void Save()
	{
		GameManager.instance.AddLevelData(this.gameObject.name, new ObjectData(possessed.gameObject.activeInHierarchy, possessed.transform.position, possessed.transform.rotation, possessed.transform.parent));
	}

	public void Load()
	{
		if(GameManager.instance.levelDictionary != null)
		{
			ObjectData loadData = new ObjectData();
			Debug.Log(gameObject.name + " | " + GameManager.instance.levelDictionary.ContainsKey(gameObject.name));
			GameManager.instance.levelDictionary.TryGetValue(this.gameObject.name, out loadData);
			LoadData(loadData);
			Debug.Log("Loading Data for " + this.name);
		}

	}
}
