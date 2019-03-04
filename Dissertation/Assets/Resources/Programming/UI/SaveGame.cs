using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveGame 
{

	public static void Save()
	{
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		scene.GetRootGameObjects(rootObjects);
		List<ISave> saves = new List<ISave>();
		foreach (GameObject gameObject in rootObjects)
		{
			saves.AddRange(Utility.GetInterface<ISave>(gameObject.GetComponentsInChildren(typeof(ISave), true)));
		}
		for(int i = 0; i < saves.Count; i++)
		{
			saves[i].Save();
		}
	}

	public static void Load()
	{
		List<GameObject> rootObjects = new List<GameObject>();
		Scene scene = SceneManager.GetActiveScene();
		Debug.Log(scene.name);
		scene.GetRootGameObjects(rootObjects);
		List<ISave> saves = new List<ISave>();
		Debug.Log("Loading Data");
		foreach (GameObject gameObject in rootObjects)
		{
			saves.AddRange(Utility.GetInterface<ISave>(gameObject.GetComponentsInChildren(typeof(ISave), true)));
		}
		for(int i = 0; i < saves.Count; i++)
		{
			saves[i].Load();
		}
		Debug.Log("Data Loaded");
	}
	
}
